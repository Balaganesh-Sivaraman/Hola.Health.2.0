﻿using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Hola.Health.MicroserviceName.Navigation;

public class MicroserviceNameToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != StandardToolbars.Main)
        {
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
