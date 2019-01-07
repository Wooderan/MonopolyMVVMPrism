using Monopoly.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Monopoly.UserField.Helpers
{
    class PlayerPreviewBehavior : Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            //this.AssociatedObject.SourceUpdated += this.OnSourceUpdated;
            this.AssociatedObject.TargetUpdated += this.OnSourceUpdated;
        }

        private void OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (e.Property == TextBlock.TextProperty)
            {
                this.BubleGumAnimate();
            }
        }

        private void BubleGumAnimate()
        {
            DoubleAnimation animationScaleX = new DoubleAnimation();
            DoubleAnimation animationScaleY = new DoubleAnimation();
            DoubleAnimation animationOpacity = new DoubleAnimation();

            animationScaleX.From = 1;
            animationScaleX.To = 1.5;
            animationScaleY.From = 1;
            animationScaleY.To = 1.5;
            animationOpacity.From = 1;
            animationOpacity.To = 0;
            animationScaleX.Duration = new Duration(TimeSpan.FromSeconds(3));
            animationScaleX.Duration = new Duration(TimeSpan.FromSeconds(3));
            animationOpacity.Duration = new Duration(TimeSpan.FromSeconds(3));

            Storyboard.SetTargetName(animationScaleX, this.AssociatedObject.Name);
            Storyboard.SetTargetName(animationScaleY, this.AssociatedObject.Name);
            Storyboard.SetTargetName(animationOpacity, this.AssociatedObject.Name);

            Storyboard.SetTargetProperty(animationScaleX, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(animationScaleY, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            Storyboard.SetTargetProperty(animationOpacity, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard animateBubleGumStoryboard = new Storyboard();
            animateBubleGumStoryboard.Children.Add(animationScaleX);
            animateBubleGumStoryboard.Children.Add(animationScaleY);
            animateBubleGumStoryboard.Children.Add(animationOpacity);

            Task.Factory.StartNew(() =>
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    animateBubleGumStoryboard.Begin(this.AssociatedObject);
                }), DispatcherPriority.Background);
            });
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SourceUpdated -= this.OnSourceUpdated;
        }
    }
}
