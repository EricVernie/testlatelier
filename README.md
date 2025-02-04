# testlatelier

## Tester l'API

Pour tester l'API, vous pouvez utiliser des outils comme `curl`, `Postman` ou `httpie`. Voici quelques exemples de commandes `curl` :

L'API est déployée dans le cloud à l'adresse suivante
https://winapi2025.azurewebsites.net/


Un swagger est disponible pour tester les endpoints
https://winapi2025.azurewebsites.net/swagger


### Tester l'endpoint GET /Players

```sh
curl -X GET http://localhost:5059/Players
curl -X GET https://winapi2025.azurewebsites.net/players/statistics
```

### Tester l'endpoint GET /Players/{id}

```sh
curl -X GET http://localhost:5059/Players/52
curl -X GET https://winapi2025.azurewebsites.net/Players/52
```

### Tester l'endpoint GET /Players/Statistics

```sh
curl -X GET http://localhost:5059/Players/Statistics
curl -X GET https://winapi2025.azurewebsites.net/Statistics
```

Assurez-vous que l'application est en cours d'exécution en utilisant la commande suivante :

```sh
dotnet run --project DemoRestAPI/DemoRestAPI.csproj
```

L'application sera disponible à l'adresse [http://localhost:5059](http://localhost:5059).

## Endpoints

### GET /Players

Retourne la liste des joueurs triée du meilleur au moins bon.

#### Réponse

- **Code :** 200 OK
- **Contenu :** Liste des joueurs triée par rang

### GET /Players/{id}

Retourne les informations d'un joueur spécifique par son ID.

#### Paramètres

- **id** (string) : L'ID du joueur

#### Réponse

- **Code :** 200 OK
- **Contenu :** Informations du joueur
- **Code :** 404 Not Found
- **Contenu :** Joueur non trouvé

### GET /Players/Statistics

Retourne des statistiques sur les joueurs, y compris :

- Le pays avec le meilleur ratio de victoires
- L'IMC moyen de tous les joueurs
- La taille médiane des joueurs

#### Réponse

- **Code :** 200 OK
- **Contenu :** Statistiques calculées

## Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}