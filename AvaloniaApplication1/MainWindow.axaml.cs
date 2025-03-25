using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media; 

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public static TextBlock x;  
    //define it outside of classes to use it in different classes
    public MainWindow()
    {
        InitializeComponent();
        var list = new List<string> { "test1", "test2", "test3" };
        x = this.FindControl<TextBlock>("Tasks");
        x.Text = string.Join('\n', list) ;
    }
    public void Add_Click(object sender, RoutedEventArgs args)
    {
        
        var inputBoxTasks = new TextBox();
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Bottom;
            Background = Brushes.Transparent;
            inputBoxTasks.AcceptsReturn = false;
            inputBoxTasks.AcceptsTab = true;
        }
        
       
        
        
        if (sender is Button btn)
        {
            ((StackPanel)(btn.Parent)).Children.Add(inputBoxTasks);
        }
        
        inputBoxTasks.KeyDown += (sender, e) =>
        {
            if (e.Key == Key.Enter)
            {
                string taskText = inputBoxTasks.Text; // Save input text to a variable
                x.Text += "\n" + taskText;
                // Remove TextBox from parent
                if (inputBoxTasks.Parent is Panel parentPanel)
                {
                    parentPanel.Children.Remove(inputBoxTasks);
                }

                e.Handled = true; // Optional: mark event as handled
            }
        };
    }
}
//FIND A WAY TO MAKE  A SOUND WHEN YOU ADD OR REMOVE A TASK

// Handle Enter key press
