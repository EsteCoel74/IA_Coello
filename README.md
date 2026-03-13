# Chat GPT 4o Mini - Application WPF

Une application WPF permettant de converser avec le modèle **GPT 4o Mini** d'OpenAI.

## Configuration

### 1. Obtenir une clé API OpenAI

1. Allez sur [https://platform.openai.com/api-keys](https://platform.openai.com/api-keys)
2. Créez une nouvelle clé API
3. Copiez la clé (vous ne pourrez pas la voir à nouveau)

### 2. Configurer la clé API

1. Ouvrez le fichier `appsettings.json`
2. Remplacez `YOUR_API_KEY_HERE` par votre clé API:
   ```json
   {
     "OpenAI": {
       "ApiKey": "sk-your-api-key-here"
     }
   }
   ```

### 3. Sécurité

- Le fichier `appsettings.json` est inclus dans `.gitignore` pour éviter de commiter votre clé API
- Ne commitez **jamais** votre clé API dans un dépôt public
- Si vous avez accidentellement commité votre clé, régénérez-la immédiatement

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

## Coûts

Attention : Chaque conversation avec GPT 4o Mini utilise des tokens et peut engendrer des coûts.
Consultez les [tarifs OpenAI](https://openai.com/pricing) pour plus de détails.
