﻿using Monopoly.BaseModel.Models;
using Monopoly.BaseModel.Views;
using Monopoly.Infrastructure.Shared;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Monopoly.BaseModel
{
    public class BaseModelModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("GameFieldRegion", typeof(GameField));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICard, TownCard>();
            containerRegistry.Register<ITaxGroup, TaxGroup>();
            containerRegistry.Register<ICardGroup, CardGroup>();
        }
    }
}