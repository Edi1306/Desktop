using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SalaFitnessModel;
using System.Data.Entity;
using System.Data;

namespace Solomon_Andrei_proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }

    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        Model1 ctx = new Model1();
        CollectionViewSource clientiVSource, AntrenoriVSource, ExercitiiVSource, SalaVSource;
        CollectionViewSource clientiProgramareVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource clientiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // clientiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource antrenoriViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("antrenoriViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // antrenoriViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource salaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("salaViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // salaViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource exercitiiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("exercitiiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // exercitiiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource clientiProgramareViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiProgramareViewSource")));
            clientiVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            clientiVSource.Source = ctx.Clientis.Local;
            ctx.Clientis.Load();

            clientiProgramareVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiProgramareViewSource")));
            clientiProgramareVSource.Source = ctx.Programares.Local;
            ctx.Clientis.Load();
            ctx.Antrenoris.Load();

            cmbClienti.ItemsSource = ctx.Clientis.Local;
            //cmbCustomers.DisplayMemberPath = "Prenume";
            cmbClienti.SelectedValuePath = "ClientiId";

            cmbAntrenori.ItemsSource = ctx.Antrenoris.Local;
           // cmbAntrenori.DisplayMemberPath = "Prenume";

            cmbAntrenori.SelectedValuePath = "Antrenori";

            AntrenoriVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("antrenoriViewSource")));
            AntrenoriVSource.Source = ctx.Antrenoris.Local;
            ctx.Antrenoris.Load();

            ExercitiiVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("exercitiiViewSource")));
            ExercitiiVSource.Source = ctx.Exercitiis.Local;
            ctx.Exercitiis.Load();

            SalaVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("salaViewSource")));
            SalaVSource.Source = ctx.Programares.Local;
            ctx.Programares.Load();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbControlSalaFit.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Clienti":
                    SaveClienti();
                    break;
                case "Antrenori":
                    SaveAntrenori();
                    break;
                case "Programare":
                    break;
            }
            ReInitialize();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            SetValidationBinding();
        }

        private void btnEdit_Copy_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);

            SetValidationBinding();
        }
        private void btnDeleteO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            clientiVSource.View.MoveCurrentToNext();
            AntrenoriVSource.View.MoveCurrentToNext();
            ExercitiiVSource.View.MoveCurrentToNext();
            SalaVSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            clientiVSource.View.MoveCurrentToPrevious();
            AntrenoriVSource.View.MoveCurrentToPrevious();
            ExercitiiVSource.View.MoveCurrentToPrevious();
            SalaVSource.View.MoveCurrentToPrevious();
        }
        private void SaveClienti()
        {
            Clienti clienti = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    clienti = new Clienti()
                    {
                        Prenume1 = prenumeTextBox.Text.Trim(),
                        Nume1 = numeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Clientis.Add(clienti);
                    clientiVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    clienti = (Clienti)clientiDataGrid.SelectedItem;
                    clienti.Prenume1 = prenumeTextBox.Text.Trim();
                    clienti.Nume1 = numeTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    clienti = (Clienti)clientiDataGrid.SelectedItem;
                    ctx.Clientis.Remove(clienti);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientiVSource.View.Refresh();
            }
        }

        private void tbControlSalaFit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveAntrenori()
        {
            Antrenori antrenori = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    antrenori = new Antrenori()
                    {
                        Prenume = prenumeTextBox.Text.Trim(),
                        Nume = numeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Antrenoris.Add(antrenori);
                    AntrenoriVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    antrenori = (Antrenori)clientiDataGrid.SelectedItem;
                    antrenori.Prenume = prenumeTextBox.Text.Trim();
                    antrenori.Nume = numeTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    antrenori = (Antrenori)antrenoriDataGrid.SelectedItem;
                    ctx.Antrenoris.Remove(antrenori);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                AntrenoriVSource.View.Refresh();
            }
        }

        private void programareDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void SaveExercitii()
        {
            Exercitii Exercitii = null;
            if (action == ActionState.New)
            {
                try
                {
                    Exercitii = new Exercitii()
                    {
                        Maini = mainiTextBox.Text.Trim(),
                        Picioare = picioareTextBox.Text.Trim(),
                        ABDOMEN = aBDOMENTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Exercitiis.Add(Exercitii);
                    ExercitiiVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    Exercitii = (Exercitii)exercitiiDataGrid.SelectedItem;
                    Exercitii.Picioare = picioareTextBox.Text.Trim();
                    Exercitii.Maini = mainiTextBox.Text.Trim();
                    Exercitii.ABDOMEN = aBDOMENTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    Exercitii = (Exercitii)exercitiiDataGrid.SelectedItem;
                    ctx.Exercitiis.Remove(Exercitii);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ExercitiiVSource.View.Refresh();
            }
        }

        private void SaveProgramare()
        {
            Programare programare = null;
            if (action == ActionState.New)
            {
                try
                {
                    Clienti clienti = (Clienti)cmbClienti.SelectedItem;
                    Antrenori Antrenori = (Antrenori)cmbAntrenori.SelectedItem;
                    //instantiem Order entity
                    programare = new Programare()
                    {

                        ClientiId = clienti.ClientiId,
                        IdAntrenor = Antrenori.AntrenoriId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Programares.Add(programare);
                    //salvam modificarile
                    ctx.SaveChanges();
                    
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                dynamic selectedOrder = clientiDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedOrder.ProgramareId;
                    var editedOrder = ctx.Programares.FirstOrDefault(s => s.ClientiId == curr_id);
                    if (editedOrder != null)
                    {
                        editedOrder.ClientiId = Int32.Parse(cmbClienti.SelectedValue.ToString());
                        editedOrder.IdAntrenor = Convert.ToInt32(cmbAntrenori.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                // pozitionarea pe item-ul curent
                clientiProgramareVSource.View.MoveCurrentTo(selectedOrder);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = antrenoriDataGrid.SelectedItem;
                    int curr_id = selectedOrder.IdProgramare;
                    var deletedOrder = ctx.Programares.FirstOrDefault(s => s.IdAntrenor == curr_id);
                    if (deletedOrder != null)   
                    {
                        ctx.Programares.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("Order Deleted Successfully", "Message");
                        
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void programareDataGridi()
        {
            var queryOrder = from pro in ctx.Programares
                             join cli in ctx.Clientis on pro.ClientiId equals
                             cli.ClientiId
                             join ant in ctx.Antrenoris on pro.IdAntrenor
                 equals ant.AntrenoriId
                             select new
                             {
                                 pro.IdProgramare,
                                 pro.IdAntrenor,
                                 pro.ClientiId,
                                 cli.Prenume1,
                                 cli.Nume1,
                                 ant.Prenume,
                                 ant.Nume
                             };
            clientiProgramareVSource.Source = queryOrder.ToList();
        }

        private void SetValidationBinding()
        {
            Binding firstNameValidationBinding = new Binding();
            firstNameValidationBinding.Source = clientiVSource;
            firstNameValidationBinding.Path = new PropertyPath("FirstName");
            firstNameValidationBinding.NotifyOnValidationError = true;
            firstNameValidationBinding.Mode = BindingMode.TwoWay;
            firstNameValidationBinding.UpdateSourceTrigger =
           UpdateSourceTrigger.PropertyChanged;
            //string required
            firstNameValidationBinding.ValidationRules.Add(new StringNotEmpty());
            prenumeTextBox.SetBinding(TextBox.TextProperty,
           firstNameValidationBinding);
            Binding lastNameValidationBinding = new Binding();
            lastNameValidationBinding.Source = clientiVSource;
            lastNameValidationBinding.Path = new PropertyPath("LastName");
            lastNameValidationBinding.NotifyOnValidationError = true;
            lastNameValidationBinding.Mode = BindingMode.TwoWay;
            lastNameValidationBinding.UpdateSourceTrigger =
           UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            lastNameValidationBinding.ValidationRules.Add(new
           StringMinLengthValidator());
            numeTextBox.SetBinding(TextBox.TextProperty,
           lastNameValidationBinding); //setare binding nou
        }

    }
}
