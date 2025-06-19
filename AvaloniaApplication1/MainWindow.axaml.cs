using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using System.Media;
namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public static TextBox? inputBoxTasks = null;

    public static TextBlock x = new();

    //define it outside of classes to use it in different classes
    public static List<string> TaskList = new List<string> { "" };
    public static ListBox listBoxTasks = new ListBox();

    private void ListBoxTasks_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        string? selectedTask = listBoxTasks.SelectedItem as string;

        if (string.IsNullOrWhiteSpace(selectedTask))
            return;

        // Remove the task from the list
        TaskList.Remove(selectedTask);

        // Update the TextBlock
        x.Text = string.Join('\n', from t in TaskList where !string.IsNullOrWhiteSpace(t) select t);

        // Remove the ListBox from the UI
        if (listBoxTasks.Parent is Panel parentPanel)
        {
            parentPanel.Children.Remove(listBoxTasks);
        }

        // Detach event to prevent firing multiple times (optional but safe)
        listBoxTasks.SelectionChanged -= ListBoxTasks_SelectionChanged;
    }

    
    public MainWindow()
    {
        InitializeComponent();
        x = this.FindControl<TextBlock>("Tasks");
        x.Text = string.Join('\n', TaskList);

    }

    public void Add_Click(object sender, RoutedEventArgs args)
    {
        if (inputBoxTasks != null && inputBoxTasks.Parent != null)
            return; // Exit early if an input box is already on screen

        inputBoxTasks = new TextBox
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            Background = Brushes.Transparent,
            AcceptsReturn = false,
            AcceptsTab = true
        };

        if (sender is Button btn && btn.Parent is StackPanel parentPanel)
        {
            parentPanel.Children.Add(inputBoxTasks);
        }

        inputBoxTasks.KeyDown += (sender, e) =>
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(inputBoxTasks.Text))
            {
                string taskText = inputBoxTasks.Text;
                TaskList.Add(taskText);
                x.Text += "\n" + taskText;

                // Remove and reset input box
                if (inputBoxTasks.Parent is Panel parentPanel)
                {
                    parentPanel.Children.Remove(inputBoxTasks);
                    inputBoxTasks = null; // reset so you can add again later
                }

                e.Handled = true;
            }
        };
    }

    

    public void Remove_Click(object sender, RoutedEventArgs args)
    {

        listBoxTasks = new ListBox()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            Background = Brushes.Transparent,
            
        };

        if (sender is Button btn)
        {
            var parentPanel = btn.Parent as StackPanel;

            // Remove old ListBox if already exists

            // Update the items
            listBoxTasks.ItemsSource = TaskList.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            listBoxTasks.SelectionMode = SelectionMode.Single;
            listBoxTasks.SelectionChanged += ListBoxTasks_SelectionChanged;


            if (!parentPanel.Children.Contains(listBoxTasks))
            {
                parentPanel.Children.Add(listBoxTasks);
            }
        }
    }

    public void ListClick(object sender, RoutedEventArgs args)
    {
        string? selectedTask = listBoxTasks.SelectedItem as string;

        if (string.IsNullOrWhiteSpace(selectedTask))
            return; // Do nothing if nothing is selected

        TaskList.Remove(selectedTask);
        x.Text = string.Join('\n', TaskList.Where(t => !string.IsNullOrWhiteSpace(t)));

        // Remove the ListBox
        if (listBoxTasks.Parent is Panel parentPanel)
        {
            parentPanel.Children.Remove(listBoxTasks);
        }
    }

        /*if button clicked, that corrosponding list element is removed from tasklist,
         and x (the textblock) is reinitialized using tasklist, and listBoxTasks should be removed.*/
    }




//FIND A WAY TO MAKE  A SOUND WHEN YOU ADD OR REMOVE A TASK

// Handle Enter key press
