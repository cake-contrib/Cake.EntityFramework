---
Order: 5
Title: How It Works
---

App configurations are essentially read at startup and some values are even read-only, hence support for dynamic Entity Framework providers is difficult. Since Cake scripts are essentially just dotNet programs, the same issues arise.

So to support multiple EF providers and configurations some hackish clever programming is required. This project uses a technique called Cross-AppDomain-Remoting which constructs the EF migrator in another app domain. By doing this we can customize which app config is read at startup among other things (which allows us to have the DbProviderFactories dynamically generated).