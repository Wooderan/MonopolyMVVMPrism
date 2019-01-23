using Monopoly.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Monopoly.GameField.Helpers
{
    class PlayerViewModelBehavior : Behavior<ContentPresenter>
    {
        public static DependencyProperty IsAttachedProperty = DependencyProperty.RegisterAttached("IsAttached", typeof(bool), typeof(PlayerViewModelBehavior), new FrameworkPropertyMetadata(false, OnIsAttachedChanged));

        private static void OnIsAttachedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var el = o as UIElement;
            if (el != null)
            {
                var behColl = Interaction.GetBehaviors(el);
                var existingBehavior = behColl.FirstOrDefault(b => b.GetType() == typeof(PlayerViewModelBehavior)) as PlayerViewModelBehavior;
                if ((bool)e.NewValue == false && existingBehavior != null)
                {
                    behColl.Remove(existingBehavior);
                }
                else if ((bool)e.NewValue == true && existingBehavior == null)
                {
                    behColl.Add(new PlayerViewModelBehavior());
                }
            }
        }

        public static bool GetIsAttached(DependencyObject o) { return (bool)o.GetValue(IsAttachedProperty); }
        public static void SetIsAttached(DependencyObject o, bool value) { o.SetValue(IsAttachedProperty, value); }

        protected override void OnAttached()
        {
            base.OnAttached();
            //PlayerViewModel playerViewModel = this.AssociatedObject.Content as PlayerViewModel;
            PlayerViewModel playerViewModel = this.AssociatedObject.DataContext as PlayerViewModel;

            NameScope.SetNameScope(this.AssociatedObject, new NameScope());
            this.AssociatedObject.Name = "p" + playerViewModel.Order.ToString();
            this.AssociatedObject.RegisterName(this.AssociatedObject.Name, this.AssociatedObject);

            if (playerViewModel != null)
            {
                playerViewModel.PropertyChanged += this.OnPVMPropertyChanged;
            }
        }

        private void OnPVMPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Pos")
            {
                PlayerViewModel playerViewModel = this.AssociatedObject.Content as PlayerViewModel;
                this.AnimateCanvasPosition(playerViewModel.Pos);
            }
        }

        private void AnimateCanvasPosition(Point point)
        {
            DoubleAnimation animationLeft = new DoubleAnimation();
            DoubleAnimation animationTop = new DoubleAnimation();

            animationLeft.To = point.X;
            animationTop.To = point.Y;
            animationLeft.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            animationTop.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            Storyboard.SetTargetName(animationLeft, this.AssociatedObject.Name);
            Storyboard.SetTargetName(animationTop, this.AssociatedObject.Name);

            Storyboard.SetTargetProperty(animationLeft, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTargetProperty(animationTop, new PropertyPath(Canvas.TopProperty));

            Storyboard animatePositionStoryboard = new Storyboard();
            animatePositionStoryboard.Children.Add(animationLeft);
            animatePositionStoryboard.Children.Add(animationTop);

            Task.Factory.StartNew(() =>
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    animatePositionStoryboard.Begin(this.AssociatedObject);
                }), DispatcherPriority.Background);
            });
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            PlayerViewModel playerViewModel = this.AssociatedObject.Content as PlayerViewModel;
            playerViewModel.PropertyChanged -= this.OnPVMPropertyChanged;
        }
    }
}
