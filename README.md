# PrismDelayedScopedRegionCreationBehavior
 
 > Allows to create scoped regions


## Initialization

Register **DelayedScopedRegionCreationBehavior** in **Container**. ServiceLocator will return this behavior for **DelayedRegionCreationBehavior**

Register **ShellService** if we want to **show multiple shells**

```cs
protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
{
    // replace DelayedRegionCreationBehavior (Prism) by DelayedScopedRegionCreationBehavior for ServiceLocator/ ContainerLocator
    containerRegistry.Register<DelayedRegionCreationBehavior, DelayedScopedRegionCreationBehavior>();
    
    // Shell Service
    containerRegistry.RegisterSingleton<IShellService, ShellService>();

    base.RegisterRequiredTypes(containerRegistry);
}
```

Add **RegionManagerAwareBehavior** (RegionBehavior) to resolve **IRegionManagerAware** for Views

```cs
protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
{
    base.ConfigureDefaultRegionBehaviors(regionBehaviors);
   
    // add RegionManagerAwareBehavior to resolve IRegionManagerAware for Views
    regionBehaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
}
```

## Show New Shell

> Inject the service and use it to show new shell

```cs
public class MainWindowViewModel
{
    private IShellService shellService;
    private DelegateCommand showShellCommand;

    public MainWindowViewModel(IShellService shellService)
    {
        if (shellService is null)
            throw new ArgumentNullException(nameof(shellService));

        this.shellService = shellService;
    }

    public DelegateCommand ShowShellCommand
    {
        get
        {
            if (showShellCommand == null)
                showShellCommand = new DelegateCommand(ExecuteShowShellCommand);
            return showShellCommand;
        }
    }

    private void ExecuteShowShellCommand()
    {
        shellService.ShowShell<MainWindow>();
    }
}
```

## IRegionManagerAware

> To get the RegionManager for the region scope

```cs
public class MainWindowViewModel : IRegionManagerAware
{
    public IRegionManager RegionManager { get; set; }
}
```

 Resources:

 * [Prism Library](https://github.com/PrismLibrary/Prism)
 * [Prism Problems & Solutions: Showing Multiple Shells](https://app.pluralsight.com/library/courses/prism-showing-multiple-shells/table-of-contents)
