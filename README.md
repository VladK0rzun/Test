# Test
Test is a WPF application that allows you to browse information about cryptocurrencies obtained through the CoinGecko API.

Running the Application
To run the application, you will need Visual Studio or another development environment that supports WPF.

Install Dependencies:
Open the project in Visual Studio and make sure all the necessary packages and dependencies are installed. If packages are missing, use the NuGet Package Manager to install them.

Set Up the CoinGecko API Key:
You will need an API key to interact with the CoinGecko API. Get your key from the CoinGecko API website and add it to the ApiKey variable in the CoinGeckoService.cs file:

csharp
Copy code
private const string ApiKey = "YOUR_API_KEY_HERE";
Run the Application:
After installing dependencies and setting up the API key, run the application. Upon launch, you will see a list of cryptocurrencies fetched from the CoinGecko API.

Using the Application
The application displays a list of cryptocurrencies with their names, price, symbol in the last 24 hours.

To view detailed information about a cryptocurrency, click on it in the list. Detailed information will be displayed in the the window.

To perform currency conversion, click on the "Convert Currency" button in the main menu. This feature allows you to convert between different cryptocurrencies or between cryptocurrencies and traditional currencies.

Disclaimer
This application is developed for educational purposes and assumes responsibility for the use of the CoinGecko API in accordance with CoinGecko's terms of use.
