# Guide de style et de collaboration 

*Projet génie logiciel 2025 : MédiathèqueWeb*

---

Ce document définit les conventions de code, les règles Git et les pratiques à suivre durant le projet.
L'objectif est de garantir la lisibilité, cohérence et qualité du code.

Sommaire : 
- [1. Organisation Git](#1-organisation-git)
    - [Arborescence des branches](#arborescence-des-branches)
    - [Workflow Git](#workflow-git)
    - [Règles de commits](#règles-de-commits)
- [2. Conventions de code](#2-conventions-de-code)
    - [Mise en forme automatique](#mise-en-forme-automatique)
    - [Nommage](#nommage)
    - [Structure des fichiers](#structure-des-fichiers)
    - [Style des classes](#style-des-classes)
- [3. Commentaires et documentation](#3-commentaires-et-documentation)
    - [À commenter](#à-commenter)
    - [À ne commenter pas](#à-ne-pas-commenter)
- [4. INTERDIT](#4-interdit)

## 1. Organisation Git 

### Arborescence des branches 

- `main` : version stable, utilisée pour la livraison des versions fonctionnelles
- `dev` : intégration continue du travail du projet
- `lina` ou `thimeo` ou `charlotte` : branches personnelles de développement

### Workflow Git

> 1. Créer une branche depuis dev et pousser la branche : 
```sh
git checkout dev
git pull
git checkout -b <prenom>
git push --set-upstream origin <prenom>
```
> 2. Développer, commits réguliers et unitaires 

**Un fois le développement de la fonctionnalité est fini :**

> 3. Pull request

> 4. Review croisée : l'autre membre lit et approuve si besoin (permet de faire des points d'avancement)

> 5. Merge si validation : Ne pas oublier de tester ! On ne merge pas une fonctionnalité qui ne fonctionne pas (sans mauvais jeu de mot).

> 6. Quand un ensemble cohérent est terminé (exigence complète) : merge dev into main et création d'une release.

### Règles de commits

Format : `[Tag] : Description claire`

Tags : 
- [Func_xx] : exigences fonctionnelles
- [Tech_xx] : exigences techniques
- [Fix] : correction
- [Refactor] : amélioration, factorisation
- [Test] : tests unitaires
- [Doc] : documentation (README, commentaires, etc.)

Exemples : 
```
[Func_03] Ajout du modèle Oeuvre et du repository
[Tech_02] Implémentation de SeedData avec données imposées
[Fix] Correction calcul dateRetourPrevue
[Test] Ajout tests PUT /emprunt/{id}
```

## 2. Conventions de code

### Mise en forme automatique

Toute modification doit être formatée avec CSharpier (voir Tech
05): `csharpier format .`

### Nommage 

- Classes, services, repositories, DTO : **PascalCase**
- Méthodes : **PascalCase**
- Variables locales, paramètres, champs privés : **camelCase**
- Constantes : **UPPER_SNAKE_CASE**

### Structure des fichiers 

- Une classe par fichier
- Un fichier au chemin correspondant à sa catégorie (Tech_04) : 
```
Model/
Services/
Repositories/
Controllers/
Exceptions/
Dtos/
```

### Style des classes

Classes métier :
- uniquement propriétés + éventuelles règles métier simples
- pas de logique complexe (réservée aux services)

Services : 
- logique métier centrale
- jamais de mapping entre Entités <-> DTO (uniquement dans les contrôleurs)

Repositories : 
- accès aux données uniquement
- aucune logique métier

Controllers : 
- validation basique
- appels aux services
- gestion des exceptions <-> codes HTTP
- mapping Entités <-> DTO
- aucune logique métier

## 3. Commentaires et documentation

### À commenter

- règles métier complexes
- validations particulières
- comportement non évident
- cas limites

### À ne pas commenter

- Code trivial
- getters/setters
- répétitions du nom de la méthode

## 4. INTERDIT

- Commit sur main (sauf release finale)
- Logique métier dans les contrôleurs
- Push sans avoir pull
- Commit avec des fichiers non formatés
- Modifications non testées
- Fichiers SQLite (data.db) dans le repo
- Code non utilisé