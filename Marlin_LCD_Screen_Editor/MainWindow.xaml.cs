using MahApps.Metro.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.SimpleChildWindow;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using ProtoBuf;
using System.Collections.Generic;
using System.Windows;
using System;
using Microsoft.Win32;

namespace Marlin_LCD_Screen_Editor
{
    public partial class MainWindow : MetroWindow
    {
        // TODO: Add & improve project support (brushes need more work)
        // TODO: Add saving of app settings (position, size, last used screen, etc)
        List<Project> ProjectList = new List<Project>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // TODO: Move these to project wizard?
        private async void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (sender == ActiveColourRect)
            //{
            //    var oldColour = ActiveColourRect.Fill;
            //    var newColour = await ChildWindowManager.ShowChildWindowAsync<Color>(this, new ChildWindows.ColourPickerWindow(oldColour));
            //    var newBrush = new SolidColorBrush(newColour);
            //    ActiveColourRect.Fill = newBrush;
            //    PixelGridControl.ActiveBrush = newBrush;
            //}
            
            //if (sender == InactiveColourRect)
            //{
            //    var oldColour = InactiveColourRect.Fill;
            //    var newColour = await ChildWindowManager.ShowChildWindowAsync<Color>(this, new ChildWindows.ColourPickerWindow(oldColour));
            //    var newBrush = new SolidColorBrush(newColour);
            //    InactiveColourRect.Fill = newBrush;
            //    PixelGridControl.InactiveBrush = newBrush;
            //}
        }

        public void SetStatusBarText(string message)
        {
            StatusBarText.Text = message;
        }

        public void SetSaveButtonVisibility(bool visible)
        {
            if (visible)
                SaveChangesButton.Visibility = Visibility.Visible;
            else
                SaveChangesButton.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            #region Create Project Button
            if (sender == CreateProjectButton)
            {
                var project = await ChildWindowManager.ShowChildWindowAsync<Project>(this, new ChildWindows.ProjectWizard());
                if (project is not null)
                {
                    // TODO: UI Improvement - pixel size and offset should be in the Settings tab
                    PixelGridControl.GenerateGrid(project);
                    project.Save();

                    if (!ProjectList.Contains(project))
                    {
                        ProjectList.Add(project);
                        ProjectDataGrid.Items.Refresh();
                    }

                    if (!AppSettings.Default.KnownProjects.Contains(project.Path))
                    {
                        AppSettings.Default.KnownProjects.Add(project.Path);
                        AppSettings.Default.Save();
                    }
                }
            }
            #endregion

            #region Load Project File Button
            if (sender == LoadProjectButton)
            {
                var ofd = new OpenFileDialog();
                ofd.Title = "Choose a Project to Load";
                ofd.Filter = "LCD Project (*.lcd)|*.lcd";

                if (ofd.ShowDialog().GetValueOrDefault())
                {
                    try
                    {
                        var file = ofd.FileName;
                        if (File.Exists(file))
                        {
                            Project loadedProject = null;

                            using(var stream = File.OpenRead(file))
                                loadedProject = Serializer.Deserialize<Project>(stream);

                            foreach(Project project in ProjectList)
                            {
                                if (project.Path == loadedProject.Path)
                                {
                                    MessageBox.Show($"The project \"{loadedProject.Name}\" is already loaded.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                            }

                            if (loadedProject is not null)
                            {
                                StatusBarText.Text = $"Project: {loadedProject.Name}";
                                AppSettings.Default.KnownProjects.Add(loadedProject.Path);
                                AppSettings.Default.Save();
                                PixelGridControl.GenerateGrid(loadedProject);
                                ProjectList.Add(loadedProject);
                                ProjectDataGrid.Items.Refresh();
                            }
                        }
                    } catch (Exception ex) {
                        Utilities.DisplayError(ex);
                    }
                }
                //if (ProjectDataGrid.SelectedIndex != -1)
                //{
                //    Project project = ProjectDataGrid.SelectedItem as Project;
                //    if (project is not null)
                //        PixelGridControl.GenerateGrid(project);
                //}
            }
            #endregion

            #region Edit Project Button
            if (sender == EditProjectButton)
            {
                if (ProjectDataGrid.SelectedIndex >= 0)
                {
                    Project selectedProject = ProjectDataGrid.SelectedItem as Project;
                    if (selectedProject is not null)
                    {
                        var editedProject = await ChildWindowManager.ShowChildWindowAsync<Project>(this, new ChildWindows.ProjectWizard(selectedProject));

                        if (editedProject is not null)
                        {
                            selectedProject = editedProject;
                            ProjectDataGrid.Items.Refresh();
                            PixelGridControl.GenerateGrid(selectedProject);
                        }
                    }
                }
            }
            #endregion

            #region Delete Project Button
            if (sender == DeleteProjectButton)
            {
                if (ProjectDataGrid.SelectedIndex >= 0)
                {
                    Project selectedProject = ProjectDataGrid.SelectedItem as Project;

                    if (selectedProject is not null)
                    {
                        ProjectList.Remove(selectedProject);
                        AppSettings.Default.KnownProjects.Remove(selectedProject.Path);
                        AppSettings.Default.Save();
                        ProjectDataGrid.Items.Refresh();

                        if (PixelGridControl.GetLoadedProject() == selectedProject)
                            PixelGridControl.UnloadActiveProject();

                        if (File.Exists(selectedProject.Path))
                        {
                            if (MessageBox.Show($"Do you want to delete the file associated with this project as well?\n\nAssociated File:\n{selectedProject.Path}", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    File.Delete(selectedProject.Path);
                                } catch (Exception ex) {
                                    Utilities.DisplayError(ex);
                                }
                            }
                        }

                        StatusBarText.Text = "Project: Nothing Loaded";
                    }
                }
            }
            #endregion

            #region Save Changes Button
            if (sender == SaveChangesButton)
                PixelGridControl.SaveLoadedProject();
            #endregion
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            if (AppSettings.Default.KnownProjects is not null && AppSettings.Default.KnownProjects.Count > 0)
            {
                Project project;

                for (int i = 0; i < AppSettings.Default.KnownProjects.Count; i++)
                {
                    using (var file = File.OpenRead(AppSettings.Default.KnownProjects[i]))
                    {
                        project = Serializer.Deserialize<Project>(file);
                        ProjectList.Add(project);
                        project.AcceptChanges();
                    }
                }

                if (ProjectList.Count > 0)
                    ProjectDataGrid.ItemsSource = ProjectList;
            }

            if (AppSettings.Default.PixelSize != 8)
                SizeNUD.Value = AppSettings.Default.PixelSize;

            if (AppSettings.Default.PixelOffset != 1)
                OffsetNUD.Value = AppSettings.Default.PixelOffset;
        }

        #region DataGrid Events
        private void ProjectDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProjectDataGrid.SelectedIndex >= 0)
            {
                EditProjectButton.IsEnabled = true;
                DeleteProjectButton.IsEnabled = true;
            } else {
                EditProjectButton.IsEnabled = false;
                DeleteProjectButton.IsEnabled = false;
            }
        }
        private void ProjectDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProjectDataGrid.SelectedIndex >= 0 && e.ChangedButton == MouseButton.Left)
            {
                Project selectedProject = ProjectDataGrid.SelectedItem as Project;

                StatusBarText.Text = $"Project: {selectedProject.Name}";
                PixelGridControl.GenerateGrid(selectedProject);
            }
        }
        #endregion

        private void NUD_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (sender == SizeNUD && SizeNUD.IsInitialized)
            {
                AppSettings.Default.PixelSize = (int)SizeNUD.Value.GetValueOrDefault();
                AppSettings.Default.Save();

                if (PixelGridControl.GetLoadedProject() is not null)
                    PixelGridControl.GenerateGrid(PixelGridControl.GetLoadedProject());
            }
            
            if (sender == OffsetNUD && OffsetNUD.IsInitialized)
            {
                AppSettings.Default.PixelOffset = (int)OffsetNUD.Value.GetValueOrDefault();
                AppSettings.Default.Save();

                if (PixelGridControl.GetLoadedProject() is not null)
                    PixelGridControl.GenerateGrid(PixelGridControl.GetLoadedProject());
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PixelGridControl.LoadedProject is not null && PixelGridControl.LoadedProject.IsChanged)
                if (MessageBox.Show("Warning\n\nYou have some pending changes, would you like to save those changes before exiting?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    PixelGridControl.LoadedProject.Save();
        }
    }
}
