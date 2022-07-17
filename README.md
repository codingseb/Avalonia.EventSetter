# Avalonia.EventSetter

## Getting started

__Install the nuget__

```nuget
dotnet add package CodingSeb.Avalonia.EventSetter
```

__For the designer__  
To make the `EventSetter` recognize by the designer you need to add the following line somewhere it's called by the designer. Example in [App.xaml.cs](Samples/App.xaml.cs)

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
