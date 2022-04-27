# VideoGameManager

## Table of Contents

## Intro

This project is a demo-project for code-alongs and future reference, to showcase just how easy it is to build a minimal WebAPI with [ASP.NET Core][asp-webapi] in [Dotnet5][dotnet5].

[asp-webapi]: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
[dotnet5]: https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5

## Entity-Framework Commands

### Make migrations

```bash
# Initial Migration: use this for the first migration you make
dotnet ef migrations add Inital
```

```bash
# All other migrations: change <MigrationName> to a fitting name (being verbose helps alot)
dotnet ef migrations add <MigrationName>
```

### Apply migrations

```bash
# Use this to apply all pending migrations and to refresh the database schema
dotnet ef database update
```
## Angular Front-End

### Creating a new Angular App

Make sure you have the latest version of [Angular CLI][angular-cli] installed.
TODO: add link to angular-cli

```bash
npm install -g @angular/cli
```

To create a new Angular project, use the command:

```bash
ng new <ProjectName>
```

Options:
- Strict-Type-Checking: [Yes|No] (for this example, we use false)
- Angular-Routing: [Yes|No] (for this example, we use true)
- Style-Sheet-Format: [Css|Scss|Sass|Less|Stylus] (for this example, we use Css)

If the command throws an error about execution policy, you need to set the execution policy to "RemoteSigned" and after that, delete the following file:
`C:\Users\<username>\AppData\Roaming\npm\ng.ps1`

```bash
# Set execution policy to RemoteSigned
Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned
```

### Getting Started with Angular

First up, in this small little example-project, we want to list all the games in the database.

To be able to query our api, we need the `HttpClientModule` from angular.

#### **`app.module.ts`**
```typescript
import { HttpClientModule } from '@angular/common/http';
```

After that, in the same file, we add it to the dependency-injection, just like we did in ASP.NET

#### **`app.module.ts`**
```typescript
@NgModule({
//...
  imports: [
    HttpClientModule
  ]
//...
})
```

Now we can write our html, which is located in `src/app/app.component.html`.
For this example, a simple table is enough.

#### **`app.component.html`**
```html
<h1>VideoGames</h1>
<table>
    <thead>
        <tr>
            <td>ID</td>
            <td>Name</td>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Foo</td>
            <td>Bar</td>
        </tr>
    </tbody>
</table>
```

Before we can add our data to the html, we need to define that data.
For that, we go into `src/app/app.component.ts` and define our data-set in TypeScript, using the same Attributes as in ASP.NET, where we used EF.

They should look something like this:

#### **`app.component.ts`**
```typescript
export interface GameGenre {
  id: number;
  name: string;
}
export interface Game {
    id: number;
    name: string;
    genre: GameGenre;
    personalRating: number;
}
```

With that, we need the code to get the data from the database.
Inside the same file, add the following code to the `export class AppComponent`:

#### **`app.component.html`**
```typescript
public games: Game[] = [];

// Don't forget the constructor, for getting the injected dependency for http-client
constructor(private http: HttpClient) {}
// This will be called when the component is initialized
// http.get() is a method from the HttpClient, which we imported in the `app.module.ts`
ngOnInit() {
  this.http.get<Game[]>('https://localhost:5001/Game').subscribe(result => {
    this.games = result;
  });
}
```

With that, we can now add the data to the table, using the `*ngFor`-directive.

#### **`app.component.html`**
```html
<h1>VideoGames</h1>
<table>
    <thead>
        <tr>
            <td>ID</td>
            <td>Name</td>
        </tr>
    </thead>
    <tbody>
        <tr *ngFor="let game of games">
            <td>{{game.id}}</td>
            <td>{{game.name}}</td>
        </tr>
    </tbody>
</table>
```

If you get a CORS-error, you need explicitly opt-in to CORS on the ASP.NET WebAPI.

## Wrap-Up

You are now officially FullStack-Developers.