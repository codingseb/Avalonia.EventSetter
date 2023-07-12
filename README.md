# Avalonia.EventSetter

| :warning: Not working anymore from version 11 of Avalonia as needed Avalonia interfaces and base classes are now internal. As an alternative, you can try [Avalonia Behaviors](https://github.com/AvaloniaUI/Avalonia.Xaml.Behaviors)
| --- |

As AvaloniaUI for now do not have an `EventSetter` like in WPF in styles. This package try to replicate the way `EventSetter` of WPF work in AvaloniaUI. 

## Getting started

__Install the nuget__

```nuget
dotnet add package CodingSeb.Avalonia.EventSetter
```

__For the designer__  
To make the `EventSetter` recognized by the designer you need to add the following line somewhere it's called by the designer. Example in [App.axaml.cs](Samples/App.axaml.cs#L12)

```c#
GC.KeepAlive(typeof(Avalonia.Styling.EventSetter).Assembly);
```

_This is an hack to force the designer to load the corresponding assembly. See issue [7200](https://github.com/AvaloniaUI/Avalonia/issues/7200) and [250](https://github.com/AvaloniaUI/AvaloniaVS/issues/250) of AvaloniaUI._

## Example

__In [MainWindow.axaml](Samples/MainWindow.axaml) :__

```axaml
<StackPanel>
    <TextBox />
    <TextBox />
    <TextBox />
	
    <StackPanel.Styles>
        <Style Selector="TextBox" >
            <EventSetter Event="KeyDown" Handler="TextBox_KeyDown" />
        </Style>
    </StackPanel.Styles>
</StackPanel>
```

__In [MainWindow.axaml.cs](Samples/MainWindow.axaml.cs) (CodeBehind) :__

```c#
private void TextBox_KeyDown(object sender, KeyEventArgs e)
{
    // What you want to do on textbox key down
}
```

## Known issues

As `Event` and `Handler` properties on the `EventSetter` are string intellisense do not work. You need to know the name of the event and the handler you want to use. also the handler must have the good signature in code behind.

As the subscribe and unsubscribe of the event is done with `Reflection` it can be slow on a large set of `StyleElements`

Attached events are not supported for now.
