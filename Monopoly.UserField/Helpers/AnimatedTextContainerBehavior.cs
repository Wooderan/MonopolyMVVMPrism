using System;
using System.Collections.Generic;
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
    class AnimatedTextContainerBehavior : Behavior<Grid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            DependencyObject element = VisualTreeHelper.GetParent(this.AssociatedObject);
            while (element.GetType() != typeof(Grid))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            Grid parentGrid = element as Grid;

            foreach (FrameworkElement item in parentGrid.Children)
            {
                if (item.Name == "increaseText" || item.Name == "decreaseText")
                {
                    item.TargetUpdated += this.OnSourceUpdated;
                }
            }
            //(this.AssociatedObject.FindName("increaseText") as TextBlock).TargetUpdated += this.OnSourceUpdated;
            //(this.AssociatedObject.FindName("decreaseText") as TextBlock).TargetUpdated += this.OnSourceUpdated;
        }

        private void OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (e.Property == TextBlock.TextProperty)
            {
                this.BubleGumAnimate(sender);
            }
        }

        private void BubleGumAnimate(object sender)
        {
            int i = 1;

            TextBlock staticText = sender as TextBlock;
            TextBlock animatedText = new TextBlock();
            animatedText.Foreground = staticText.Foreground;
            animatedText.FontSize = staticText.FontSize;
            animatedText.FontWeight = staticText.FontWeight;
            animatedText.Name = "animatedText" + i++;
            animatedText.RenderTransform = new ScaleTransform();
            
            NameScope.SetNameScope(this.AssociatedObject, new NameScope());
            this.AssociatedObject.RegisterName(animatedText.Name, animatedText);
            var result = this.AssociatedObject.FindName(animatedText.Name);
            //BehaviorCollection behaviors = Interaction.GetBehaviors(animatedText);
            //behaviors.Add(new PlayerPreviewBehavior());
            animatedText.Text = staticText.Text;

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

            Storyboard.SetTargetName(animationScaleX, animatedText.Name);
            Storyboard.SetTargetName(animationScaleY, animatedText.Name);
            Storyboard.SetTargetName(animationOpacity, animatedText.Name);

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
            (this.AssociatedObject.FindName("increaseText") as TextBlock).TargetUpdated -= this.OnSourceUpdated;
            (this.AssociatedObject.FindName("decreaseText") as TextBlock).TargetUpdated -= this.OnSourceUpdated;
        }
    }
}
