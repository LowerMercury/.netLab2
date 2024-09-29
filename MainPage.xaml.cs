using Lab1;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab2
{
    public partial class MainPage : ContentPage
    {
        IBusinessLogic businessLogic = new BusinessLogic();

        public MainPage()
        {
            InitializeComponent();
            foreach (Lab1.Airport airport in businessLogic.GetAirports())
            {
                AddAirportToGrid(airport);
            }
        }

        // Helper method to create and add the airport row to the grid
        private void AddAirportToGrid(Lab1.Airport airport)
        {
            Label idLabel = CreateLabel(airport.Id);
            Label cityLabel = CreateLabel(airport.City);
            Label dateLabel = CreateLabel(airport.DateVisited.ToString());
            Label ratingLabel = CreateLabel(airport.Rating.ToString());

            Button deleteButton = new Button()
            {
                ImageSource = "trashcan.png",
                Background = Brush.Transparent,
                Scale = 0.1,
                VerticalOptions = LayoutOptions.Center
            };
            deleteButton.Clicked += DeleteAirport_Click; // Attach functionality to button

            // Add the elements to the grid row
            int currRow = Airport_Grid.Count;
            Airport_Grid.Add(idLabel, 0, currRow);
            Airport_Grid.Add(cityLabel, 1, currRow);
            Airport_Grid.Add(dateLabel, 2, currRow);
            Airport_Grid.Add(ratingLabel, 3, currRow);
            Airport_Grid.Add(deleteButton, 4, currRow);
        }

        // Helper method to create a Label
        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                VerticalOptions = LayoutOptions.Center
            };
        }

        private void AddAirport_Click(object sender, EventArgs e)
        {
            DateTime date;
            AirportInputError errorCode = AirportInputError.NoError;
            if(ratingEntry.Text == null || ratingEntry.Text.Equals(""))
            {
                errorCode = AirportInputError.RatingOutOfBounds;
            } 
            else if (dateEntry.Text == null || dateEntry.Text.Equals("") || !DateTime.TryParse(dateEntry.Text, out date)){
                errorCode = AirportInputError.InvalidDate;
            }
            else
            {
                // Parsing inputs and adding airport
                errorCode = businessLogic.AddAirport(
                    idEntry.Text, cityEntry.Text, DateTime.Parse(dateEntry.Text), int.Parse((ratingEntry.Text)));
            }   

            if (errorCode == AirportInputError.NoError)
            {
                // Reuse the AddAirportToGrid method
                AddAirportToGrid(new Lab1.Airport(idEntry.Text, cityEntry.Text, DateTime.Parse(dateEntry.Text), int.Parse(ratingEntry.Text)));
            }
            else
            {
                // Handle errors
                HandleError(errorCode);
            }
        }

        // Error handling method
        private void HandleError(AirportInputError errorCode)
        {
            string errorMessage = errorCode switch
            {
                AirportInputError.DuplicateAirportId => "Airport Already Exists",
                AirportInputError.InvalidIDLength => "Id should be a length of 3-4 characters",
                AirportInputError.RatingOutOfBounds => "Rating should be 1 - 5",
                AirportInputError.InvalidCityLength => "City should be less than 25 characters",
                AirportInputError.InvalidDate => "Date is Malformed",
                AirportInputError.DBAdditionError => "Airport Not Added to Database",
                _ => "Error: unable to add airport"
            };

            DisplayAlert("Error", errorMessage, "Cancel");
        }

        private void EditAirport_Click(object sender, EventArgs e)
        {
            DateTime date;
            AirportEditError errorCode = AirportEditError.NoError;
            if (ratingEntry.Text == null || ratingEntry.Text.Equals(""))
            {
                errorCode = AirportEditError.RatingOutOfBounds;
            }
            else if (dateEntry.Text == null || dateEntry.Text.Equals("") || !DateTime.TryParse(dateEntry.Text, out date))
            {
                errorCode = AirportEditError.InvalidDate;
            }
            else
            {
                // sends the airport edit to the backend
                errorCode = businessLogic.EditAirport(idEntry.Text, cityEntry.Text, DateTime.Parse(dateEntry.Text), Int32.Parse(ratingEntry.Text));
            }
            if (errorCode == AirportEditError.NoError) // if id was found
            {
                // edit the entries in the grid
                int row = Airport_Grid.GetRow(Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetColumn(c) == 0 && ((Label)c).Text.Equals(idEntry.Text))); // gets the row of the entered ID
                ((Label)Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetRow(c) == row && Airport_Grid.GetColumn(c) == 0)).Text = idEntry.Text; // find and edit the id
                ((Label)Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetRow(c) == row && Airport_Grid.GetColumn(c) == 1)).Text = cityEntry.Text;// find and edit the city
                ((Label)Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetRow(c) == row && Airport_Grid.GetColumn(c) == 2)).Text = dateEntry.Text; // find and edit the date
                ((Label)Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetRow(c) == row && Airport_Grid.GetColumn(c) == 3)).Text = ratingEntry.Text; // find and edit the rating
            }
            else if (errorCode == AirportEditError.IdNotPresent)
            {
                DisplayAlert("Error", "Id not found in database", "cancel");
            }
            else
            {
                DisplayAlert("Error","Error: unable to edit airport","cancel");
            }
        }

        // !!!!!! This still needs to reorder the airports once one is deleted
        private void DeleteAirport_Click(object sender, EventArgs e)
        {

            var row = Airport_Grid.GetRow((Button)sender);
            Label idLabel = ((Label)Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetColumn(c) == 0 && Airport_Grid.GetRow(c) == row));
            if (idLabel != null) {
                AirportDeletionError errorCode = businessLogic.DeleteAirport(idLabel.Text);
                if (errorCode == AirportDeletionError.NoError)
                {
                    for (int col = 0; col < Airport_Grid.ColumnDefinitions.Count; col++)
                    {

                        var child = Airport_Grid.Children.FirstOrDefault(c => Airport_Grid.GetRow(c) == row && Airport_Grid.GetColumn(c) == col);
                        if (child != null)
                        {
                            Airport_Grid.Children.Remove(child);

                        }
                    }
                }
                else if(errorCode == AirportDeletionError.AirportNotFound)
                {
                    DisplayAlert("Error", "Airport not present in database", "cancel");
                }
                else if (errorCode == AirportDeletionError.FailedToDeleteError)
                {
                    DisplayAlert("Error", "Failed to Delete Airport", "cancel");
                }
            }
            else
            {
                DisplayAlert("Error", "No ID Provided", "cancel");
            }
        }
    }
}
