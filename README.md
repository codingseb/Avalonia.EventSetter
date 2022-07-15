# Avalonia.EventSetter

## Example

__In [MainWindow.axaml](Samples/MainWindow.axaml) :__

```axaml
<StackPanel>
	<TextBox Text="0" />
	<TextBox Text="0" />
	<TextBox Text="0" />
		
	<StackPanel.Styles>
		<Style Selector="TextBox" >
			<Setter Property="Margin" Value="2" />
			<EventSetter Event="KeyDown" Handler="TextBox_KeyDown" />
		</Style>
	</StackPanel.Styles>
</StackPanel>
```

__In MainWindow.axaml.cs (CodeBehind) :__

```c#
private void TextBox_KeyDown(object sender, KeyEventArgs e)
{
    if(e.Source is TextBox textBox)
    {
        if(e.Key == Key.Down)
        {
            if(double.TryParse(textBox.Text, out double value));
            {
                if(value <= 0)
                {
                    textBox.Text = "0";
                }
                else
                {
                    textBox.Text = (value - 1).ToString();
                }
            }

            textBox.SelectAll();
        }
        else if(e.Key == Key.Up)
        {
            if(double.TryParse(textBox.Text, out double value));
            {
                textBox.Text = (value + 1).ToString();
            }

            textBox.SelectAll();
        }
    }
}
```
