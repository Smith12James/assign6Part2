using Assignment6AirlineReservation.Common;
using Assignment6AirlineReservation.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// initialize window to add passengers
        /// </summary>
        wndAddPassenger wndAddPass;

        /// <summary>
        /// initialize flightManager class
        /// </summary>
        clsFlightManager flightManager = new clsFlightManager();
        /// <summary>
        /// initialize passengerManager class
        /// </summary>
        clsPassengerManager passengerManager = new clsPassengerManager();

        /// <summary>
        /// initialize passengerList to query at a later time.
        /// </summary>
        List<clsPassengers> passengerList;

        /// <summary>
        /// current passenger label
        /// </summary>
        Label lblPassenger;

        /// <summary>
        /// current passenger using generic data type clsPassengers
        /// </summary>
        clsPassengers currentPassenger;

        /// <summary>
        /// current flight ID based on combobox selection
        /// </summary>
        int iFlightID;

        /// <summary>
        /// all current filled seats array.
        /// </summary>
        string[] sFilledSeatsArr;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                flightCmbobx();
                
                /// original code below commented out as I've moved the logic to a different class

/*                DataSet ds = new DataSet();
                //Should probably not have SQL statements behind the UI
                string sSQL = clsSQL.selectFlightInfo();
                int iRet = 0;
                clsData = new clsDataAccess();

                //This should probably be in a new class.  Would be nice if this new class
                //returned a list of Flight objects that was then bound to the combo box
                //Also should show the flight number and aircraft type together
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                //Should probably bind a list of flights to the combo box
                for(int i = 0; i < iRet; i++)
                {
                    cbChooseFlight.Items.Add(ds.Tables[0].Rows[i][0]);
                }*/
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            }

        }

        /// <summary>
        /// method is used to populate the flight combobox. This gets a list of flights from the clsFlightManager class
        /// and return a List<clsFlights> to bind to the combo box
        /// </summary>
        public void flightCmbobx()
        {
            try
            {
                // initialize clsFlights list
                List<clsFlights> flights = flightManager.GetFlights();

                if (flights.Count < 1)
                {
                    throw new Exception("Error gathering flight info.");

                }
                else
                {
                    cbChooseFlight.ItemsSource = flights;

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// method is used to detect a selection change, and update the UI based on the flight ID that is bound to the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmdDeletePassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;

                cbChoosePassenger.SelectionChanged -= cbChoosePassenger_SelectionChanged;
                cbChoosePassenger.ItemsSource = null;
                cbChoosePassenger.Items.Clear();
                cbChoosePassenger.SelectedIndex = -1;
                cbChoosePassenger.SelectionChanged += cbChoosePassenger_SelectionChanged;

                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                
                lblPassengersSeatNumber.Content = "";

                this.lblPassenger = null;

                clsFlights selectedFlight = cbChooseFlight.SelectedItem as clsFlights;

                if (selectedFlight != null)
                {
                    //this.iFlightID = selectedFlight.iFlightID;

                    passengerCmbobx(selectedFlight.iFlightID);

                    switch (iFlightID)
                    {
                        case 1:
                            CanvasA380.Visibility = Visibility.Visible;
                            Canvas767.Visibility = Visibility.Hidden;

                            break;

                        case 2:
                            Canvas767.Visibility = Visibility.Visible;
                            CanvasA380.Visibility = Visibility.Hidden;

                            break;

                    }

                }
                else
                {
                    throw new Exception("Unable to obtain Passenger Info");

                }

                /// commenting out wrong assignment instructions logic

                //string selection = cbChooseFlight.SelectedItem.ToString();  //This is wrong, if a list of flights was in the combo box, then could get the selected flight in an object

                ///commented the below to move logic to a different class and make code more simple and easier to read

                /*                DataSet ds = new DataSet();                
                                int iRet = 0;

                                //Should be using a flight object to get the flight ID here
                                if (selection == "1")
                                {
                                    CanvasA380.Visibility = Visibility.Hidden;
                                    Canvas767.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    Canvas767.Visibility = Visibility.Hidden;
                                    CanvasA380.Visibility = Visibility.Visible;
                                }

                                //I think this should be in a new class to hold SQL statments
                                string sSQL = clsSQL.selectPassengerInfo(int.Parse(cbChooseFlight.SelectedItem.ToString()));//If the cbChooseFlight was bound to a list of Flights, the selected object would have the flight ID
                                //Probably put in a new class
                                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                                cbChoosePassenger.Items.Clear();//Don't need if assigning a list of passengers to the combo box

                                //Would be nice if code from another class executed the SQL above, added each passenger into a Passenger object,
                                //then into a list of Passengers to be returned and bound to the combo box
                                for (int i = 0; i < iRet; i++)
                                {
                                    cbChoosePassenger.Items.Add(ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                                }*/

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this method will populate the passenger combo box.
        /// </summary>
        private void passengerCmbobx(int iFlight)
        {
            try
            {
                this.iFlightID = iFlight;

                // if passengerList is not null clear all items
                if (passengerList != null)
                {
                    passengerList.Clear();

                }

                passengerList = passengerManager.GetPassengers(iFlight);
                cbChoosePassenger.ItemsSource = passengerList;

                cbChoosePassenger.Items.Refresh();

                switch (iFlight)
                {
                    case 1:
                        fillSeats("SeatA", passengerList);

                        break;

                    case 2:
                        fillSeats("Seat", passengerList);

                        break;

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// if passenger selection is changed, show the current passengers name, seat number, and highlight seat green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmdDeletePassenger.IsEnabled = true;
                cmdChangeSeat.IsEnabled = true;

                if (this.lblPassenger != null)
                {
                    this.lblPassenger.Background = Brushes.Red;

                }

                clsPassengers passenger = cbChoosePassenger.SelectedItem as clsPassengers;
                currentPassenger = passenger;

                if(passenger == null)
                {
                    string sSelectedSeat = this.lblPassenger.Content.ToString();

                    foreach (clsPassengers passengerLoop in cbChoosePassenger.Items)
                    {
                        if (passengerLoop.sSeatNumber == sSelectedSeat)
                        {
                            cbChoosePassenger.SelectedItem = passengerLoop;
                            currentPassenger = passengerLoop;
                            passenger = passengerLoop;

                            break;

                        }

                    }

                    if (cbChoosePassenger.SelectedIndex != -1)
                    {
                        return;

                    }

                    if (currentPassenger == null)
                    {
                        throw new Exception("Unable to locate passenger");

                    }

                }

                if (passenger.sSeatNumber != "0")
                {
                    // again used as the grids used have different naming conventions. Seat1 vs SeatA1
                    switch (this.iFlightID)
                    {
                        case 1:
                            this.lblPassenger = (Label)this.FindName($"SeatA{passenger.sSeatNumber}");
                            this.lblPassenger.Background = Brushes.ForestGreen;

                            break;

                        case 2:
                            this.lblPassenger = (Label)this.FindName($"Seat{passenger.sSeatNumber}");
                            this.lblPassenger.Background = Brushes.ForestGreen;

                            break;

                    }

                }
                else if (passenger.sSeatNumber == "0")
                {
                    changeSeatFunction();

                }

                lblPassengersSeatNumber.Content = passenger.sSeatNumber;

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            

        }

        /// <summary>
        /// method will be called to fill in current seats occupied on the selected flight. 
        /// This will take a list of clsPassengers as a parameter to iterate through and update the seat color.
        /// input sSeat is meant to be used to detect which flight to query as original creator has the first grid assigned as Seat1, Seat2, Seat3.
        /// And has the second grid as SeatA1, SeatA2, SeatA3
        /// </summary>
        private void fillSeats(string sSeat, List<clsPassengers> passengerList)
        {
            try
            {
                foreach (clsPassengers passenger in passengerList)
                {
                    if (passenger.sSeatNumber != "0")
                    {
                        string sSeatNum = passenger.sSeatNumber;

                        Label lblFilledSeats = (Label)this.FindName($"{sSeat}{sSeatNum}");

                        if (lblFilledSeats != null)
                        {
                            lblFilledSeats.Background = Brushes.Red;

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// method used to show the add a passenger window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger(this.iFlightID);
                wndAddPass.ShowDialog();

                passengerCmbobx(this.iFlightID);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this handles all logic pertaining to changing passenger seat
        /// </summary>
        private void changeSeat(int iPassengerID, string sSeatNumber)
        {
            try
            {
                cmdAddPassenger.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;

                clsChangeSeat clsChangeSeat = new clsChangeSeat();

                clsChangeSeat.changeSeat(this.iFlightID, iPassengerID, sSeatNumber);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Button requires sender and routedEventArgs, all logic is handled in changeSeatFunction() 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                changeSeatFunction();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }

        /// <summary>
        /// called when change seat button is clicked or selected passenger seat is unassigned (aka seat number 0). Method will change all unoccupied seats click function.
        /// </summary>
        private void changeSeatFunction()
        {
            try
            {
                int iSeatCount = 0;
                string sSeatFlightHeader = "";

                sFilledSeatsArr = new string[cbChoosePassenger.Items.Count];

                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;

                switch (this.iFlightID)
                {
                    case 1:
                        iSeatCount = 15;
                        sSeatFlightHeader = "SeatA";

                        break;

                    case 2:
                        iSeatCount = 16;
                        sSeatFlightHeader = "Seat";

                        break;

                }

                if (this.currentPassenger.sSeatNumber != "0")
                {
                    Label lblCurrentSeat = (Label)this.FindName($"{sSeatFlightHeader}{this.currentPassenger.sSeatNumber}");
                    lblCurrentSeat.Background = Brushes.Blue;

                }

                this.currentPassenger.sSeatNumber = "0";

                int iTempCount = 0;

                foreach (clsPassengers passenger in cbChoosePassenger.Items)
                {
                    sFilledSeatsArr[iTempCount] = passenger.sSeatNumber;

                    iTempCount++;

                }

                for (int i = 0; i < iSeatCount; i++)
                {

                    if (!sFilledSeatsArr.Contains(i.ToString()))
                    {
                        Label lblFilledSeats = (Label)this.FindName($"{sSeatFlightHeader}{i}");

                        lblFilledSeats.MouseLeftButtonUp -= Seati_MouseLeftButtonUp;
                        lblFilledSeats.MouseLeftButtonUp += changeSeatMouseClick;


                    }

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Method used to handle errors and exceptions, will show a messagebox unless that fails, in which case this will create a text file with error info.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// method is used to capture a mouse click on the release of the button. This is for the first grid which should exclude the A in SeatA1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seati_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label lblClicked = sender as Label;

                string lblName = lblClicked.Content.ToString();

                cbChooseFlight.IsEnabled = true;
                cbChoosePassenger.IsEnabled = true;
                cmdChangeSeat.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                cmdDeletePassenger.IsEnabled = true;

                foreach (clsPassengers passenger in cbChoosePassenger.ItemsSource)
                {
                    if (passenger.sSeatNumber == lblName)
                    {
                        cbChoosePassenger.SelectedItem = passenger;
                        break;

                    }

                }

                //lblPassengersSeatNumber.Content = lblName;

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }

        /// <summary>
        /// change seat when the user clicks on the label to select the seat for either a new passenger or an existing passenger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeSeatMouseClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label lblClicked = sender as Label;

                this.lblPassenger = lblClicked;

                string lblName = lblClicked.Content.ToString();

                int iSeatCount = 0;
                string sSeatFlightHeader = "";

                changeSeat(this.currentPassenger.iPassengerID, lblName);
                passengerCmbobx(this.iFlightID);

                switch (this.iFlightID)
                {
                    case 1:
                        iSeatCount = 15;
                        sSeatFlightHeader = "SeatA";

                        break;

                    case 2:
                        iSeatCount = 16;
                        sSeatFlightHeader = "Seat";

                        break;

                }

                cbChoosePassenger.SelectionChanged -= cbChoosePassenger_SelectionChanged;
                cbChoosePassenger.SelectedIndex = -1;
                cbChoosePassenger.SelectionChanged += cbChoosePassenger_SelectionChanged;

                /*foreach (clsPassengers passenger in cbChoosePassenger.Items)
                {
                    if (passenger.sSeatNumber == lblName)
                    {
                        currentPassenger = passenger;

                        cbChoosePassenger.SelectedItem = passenger;
                    }

                }*/

                Label lblNewSeat = (Label)this.FindName($"{sSeatFlightHeader}{lblName}");
                lblNewSeat.Background = Brushes.Red;

                for (int i = 0; i < iSeatCount; i++)
                {
                    Label lblFilledSeats = (Label)this.FindName($"{sSeatFlightHeader}{i}");
                    if (lblFilledSeats != null && !sFilledSeatsArr.Contains(i.ToString()))
                    {
                        lblFilledSeats.MouseLeftButtonUp += Seati_MouseLeftButtonUp;
                        lblFilledSeats.MouseLeftButtonUp -= changeSeatMouseClick;

                    }

                }

                cbChooseFlight.IsEnabled = true;
                cbChoosePassenger.IsEnabled = true;
                cmdChangeSeat.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                cmdDeletePassenger.IsEnabled = true;
                lblPassengersSeatNumber.Content = lblName;



            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// delete currently selected passenger in combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsDeletePassenger deletePassenger = new clsDeletePassenger();

                clsPassengers passengerToDelete = cbChoosePassenger.SelectedItem as clsPassengers;

                //deletePassenger.deletePassenger(passengerToDelete.iPassengerID, this.iFlightID);

                string sSeatFlightHeader = "";
                string sPassengerSeatNum = passengerToDelete.sSeatNumber;

                switch (this.iFlightID)
                {
                    case 1:
                        sSeatFlightHeader = "SeatA";

                        break;

                    case 2:
                        sSeatFlightHeader = "Seat";

                        break;

                }

                cbChoosePassenger.SelectionChanged -= cbChoosePassenger_SelectionChanged;
                cbChoosePassenger.ItemsSource = null;
                cbChoosePassenger.Items.Clear();
                cbChoosePassenger.SelectedIndex = -1;
                cbChoosePassenger.SelectionChanged += cbChoosePassenger_SelectionChanged;

                Label lblPassengerSeat = (Label)this.FindName($"{sSeatFlightHeader}{sPassengerSeatNum}");

                lblPassengerSeat.Background = Brushes.Blue;

                deletePassenger.deletePassenger(passengerToDelete.iPassengerID, this.iFlightID);

                passengerCmbobx(this.iFlightID);

                this.currentPassenger = null;

                cbChooseFlight.SelectionChanged -= cbChooseFlight_SelectionChanged;
                cbChooseFlight.SelectedIndex = -1;
                cbChooseFlight.SelectionChanged += cbChooseFlight_SelectionChanged;

                foreach (clsFlights flight in cbChooseFlight.Items)
                {
                    if (this.iFlightID == flight.iFlightID)
                    {
                        cbChooseFlight.SelectedItem = flight;
                        cbChooseFlight.UpdateLayout();

                    }

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
    }
}
