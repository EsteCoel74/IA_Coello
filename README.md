# Chat GPT 4o Mini - Application WPF

Une application WPF permettant de converser avec le modèle **GPT 4o Mini** d'OpenAI.


##Sécurité

- Le fichier `appsettings.json` est inclus dans `.gitignore` pour éviter de commiter la clé clé API
- Ne commitez **jamais** la clé API dans un dépôt public

## Utilisation

1. Compilez et lancez l'application
2. Tapez votre message dans le champ de texte
3. Cliquez sur "Envoyer" ou appuyez sur Entrée
4. Attendez la réponse de ChatGPT
5. Utilisez "Effacer" pour réinitialiser la conversation

## Prérequis

- .NET 10.0 ou supérieur
- Visual Studio 2026 (ou un autre IDE compatible)
- Une clé API OpenAI active

## Architecture

- **Services/ChatGptService.cs** : Service de communication avec l'API OpenAI
- **Configuration/AppSettings.cs** : Gestion de la configuration
- **MainWindow.xaml/cs** : Interface utilisateur WPF

