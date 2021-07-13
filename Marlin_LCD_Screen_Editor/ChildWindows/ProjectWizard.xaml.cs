using MahApps.Metro.SimpleChildWindow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace Marlin_LCD_Screen_Editor.ChildWindows
{
    public partial class ProjectWizard : ChildWindow
    {
        bool editMode = false;
        Project editProject;

        public ProjectWizard()
        {
            InitializeComponent();
        }

        public ProjectWizard(Project project)
        {
            InitializeComponent();

            editMode = true;
            editProject = project;

            ProjectNameTB.Text = project.Name;
            AuthorTB.Text = project.Author;
            ProjectPathTB.Text = project.Path;

            if (project.ScreenData.Width == 88 && project.ScreenData.Height == 58 && project.ScreenData.Columns == 11 && project.ScreenData.Rows == 58)
            {
                ScreenSelector.SelectedItem = Ender3Screen;
            } else {
                ScreenSelector.SelectedItem = CustomScreen;
                WidthNUD.Value = project.ScreenData.Width;
                HeightNUD.Value = project.ScreenData.Height;
                ColumnNUD.Value = project.ScreenData.Columns;
                RowsNUD.Value = project.ScreenData.Rows;
                NUDContainer.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == PathButton)
            {
                // TODO: UX Improvement - default path for projects in Settings tab?
                string projectName = ProjectNameTB.Text;
                var sfd = new SaveFileDialog();
                if (!String.IsNullOrWhiteSpace(projectName))
                    sfd.FileName = $"{ProjectNameTB.Text}.lcd";
                else
                    sfd.FileName = $"New LCD Project.lcd";
                sfd.Filter = "LCD Project (*.lcd)|*.lcd";

                var result = sfd.ShowDialog();

                if (result.GetValueOrDefault())
                {
                    ProjectPathTB.Text = sfd.FileName;

                    if (String.IsNullOrWhiteSpace(ProjectNameTB.Text))
                    {
                        if (sfd.FileName.Contains(".lcd"))
                            ProjectNameTB.Text = sfd.SafeFileName.Replace(".lcd", "");
                        else
                            ProjectNameTB.Text = sfd.FileName;
                    }
                }
            }

            if (sender == ConfirmButton)
            {
                if (editMode)
                {
                    if (editProject is not null)
                    {
                        editProject.Name = ProjectNameTB.Text;
                        editProject.Path = ProjectPathTB.Text;
                        editProject.Author = AuthorTB.Text;
                        editProject.ScreenData = new ScreenData((int)WidthNUD.Value, (int)HeightNUD.Value, (int)ColumnNUD.Value, (int)RowsNUD.Value, Brushes.Aquamarine, Brushes.DodgerBlue);
                        
                        Close(editProject);
                    }
                } else {
                    Project newProject = new Project(ProjectNameTB.Text);

                    if (ScreenSelector.SelectedItem == Ender3Screen)
                        newProject.ScreenData = new ScreenData(88, 58, 11, 58, Brushes.Aquamarine, Brushes.DodgerBlue);

                    if (ScreenSelector.SelectedItem == CustomScreen)
                        newProject.ScreenData = new ScreenData((int)WidthNUD.Value, (int)HeightNUD.Value, (int)ColumnNUD.Value, (int)RowsNUD.Value, Brushes.Aquamarine, Brushes.DodgerBlue);

                    if (!String.IsNullOrWhiteSpace(AuthorTB.Text))
                        newProject.Author = AuthorTB.Text;

                    if (!String.IsNullOrWhiteSpace(ProjectPathTB.Text))
                        newProject.Path = ProjectPathTB.Text;

                    if (newProject.IsValid())
                        Close(newProject);
                    else
                        MessageBox.Show("Unable to save project, some required fields are either invalid or empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if (sender == CancelButton)
                Close();
        }

        private void ScreenSelector_DropDownClosed(object sender, EventArgs e)
        {
            if (ScreenSelector.SelectedItem == CustomScreen)
            {
                NUDContainer.IsEnabled = true;
            } else {
                NUDContainer.IsEnabled = false;
                WidthNUD.Value = 88;
                HeightNUD.Value = 58;
                ColumnNUD.Value = 11;
                RowsNUD.Value = 58;
            }
        }
    }
}
