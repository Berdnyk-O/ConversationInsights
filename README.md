# ConversationInsights

This API allows you to analyze audio files, extract key information such as names and locations, categorize conversations into appropriate categories, and determine emotional tone.

## Key Features

- **Onion Architecture**: The application is structured using Onion Architecture, promoting clean code and separation of concerns.
- **PostgreSQL Database**: Data is stored and managed using a PostgreSQL database, providing a robust and scalable storage solution.
- **Minimal API**: The API is built using the Minimal API approach, enabling a streamlined and efficient way to define HTTP routes and request handling.
- **Vosk Models**: Utilizes Vosk models for offline audio-to-text conversion, allowing for accurate transcription of spoken content.
- **VaderSharp2**: provides an effective way to evaluate the sentiment of text data, including the intensity of emotions expressed in conversations.

## Executing program

1. Clone the repository
2. Open the **appsettings.json** and update the **DefaultConnection**.
3. Create the **Audios** and **VoskModel** folders in the application layer.
4. Download the model from the [official website](https://alphacephei.com/vosk/models) and unzip it to the VoskModel folder.((folders am, conf, graph, invector))  
5. Start the application(Upon the first launch of the application, the MigrationHostedService will automatically apply all database migrations).

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.
