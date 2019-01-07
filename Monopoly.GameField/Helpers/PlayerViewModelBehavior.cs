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
        protected override void OnAttached()
        {
            base.OnAttached();
            PlayerViewModel playerViewModel = this.AssociatedObject.Content as PlayerViewModel;
            if (playerViewModel != null)
            {
                playerViewModel.PropertyChanged += this.OnPVMPropertyChanged;
            }
            else
            {
                throw new Exception("Content is not PlayerViewModel!");
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
