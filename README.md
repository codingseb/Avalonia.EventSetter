# Avalonia.EventSetter

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
  // What you want to do on textbox click
}
```
