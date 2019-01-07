using Monopoly.GameField.ViewModels;
using Monopoly.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Monopoly.GameField.Helpers
{
    class PlayerTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PlayerTemplate1 { get; set; }
        public DataTemplate PlayerTemplate2 { get; set; }
        public DataTemplate PlayerTemplate3 { get; set; }
        public DataTemplate PlayerTemplate4 { get; set; }
        public DataTemplate PlayerTemplate5 { get; set; }
        public DataTemplate PlayerTemplate6 { get; set; }
        public DataTemplate MockPlayerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            PlayerViewModel viewModel = item as PlayerViewModel;
            if (viewModel != null)
            {
                switch (viewModel.Order)
                {
                    case 1:
                        return PlayerTemplate1;
                    case 2:
                        return PlayerTemplate2;
                    case 3:
                        return PlayerTemplate3;
                    case 4:
                        return PlayerTemplate4;
                    case 5:
                        return PlayerTemplate5;
                    case 6:
                        return PlayerTemplate6;
                    default:
                        return MockPlayerTemplate;
                }

            }
            else
            {
                return null;
            }
        }
    }
}
