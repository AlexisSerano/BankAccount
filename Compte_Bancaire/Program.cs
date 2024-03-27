using System;
using System.Collections.Generic;
using System.Security.Principal;

public class Compte_Bancaire
{
    public string Pseudo { get; set; }
    public string Password { get; set; }
    public double Solde { get; set; }

    public Compte_Bancaire(string pseudo, string password)
    {
        Pseudo = pseudo;
        Password = password;
        Solde = 0;
    }
}

class Banque
{
    public void label(Dictionary<string, Compte_Bancaire> account, string pseudo)
    {
        while (true)
        {
            try
            {
                Console.WriteLine($"Bienvenue dans votre banque {pseudo}");
                Console.WriteLine("1 - déposer");
                Console.WriteLine("2 - retirer");
                Console.WriteLine("3 - voir_solde");
                Console.WriteLine("4 - Déconnexion");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        deposer(account, pseudo);
                        break;
                    case 2:
                        retirer(account, pseudo);
                        break;
                    case 3:
                        voir_solde(account, pseudo);
                        break;
                    case 4:
                        return;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Entrée invalide");
            }
        }

    }

    public void deposer(Dictionary<string, Compte_Bancaire> account, string pseudo)
    {
        try
        {
            Console.WriteLine("Combien voulez vous déposer ?");
            double deposer = double.Parse(Console.ReadLine());
            account[pseudo].Solde += deposer;
            Console.WriteLine($"Somme de {deposer} déposer");
        }
        catch
        {
            Console.WriteLine("Une erreur est survenue");
        }
    }

    public void retirer(Dictionary<string, Compte_Bancaire> account, string pseudo)
    {
        try
        {
            if (account[pseudo].Solde == 0)
            {
                Console.WriteLine("Votre compte est vide");
            }

            Console.WriteLine("Combien voulez vous retirer");
            double retirer = double.Parse(Console.ReadLine());
            if (account[pseudo].Solde - retirer < 0)
            {
                Console.WriteLine("Vous n'avez pas assez pour retirer cette somme");
            }
            else
            {
                account[pseudo].Solde -= retirer;
                Console.WriteLine($"La somme de {retirer} a été retier");
            }
        }
        catch
        {
            Console.WriteLine("Une erreur est survenue");
        }

    }

    public void voir_solde(Dictionary<string, Compte_Bancaire> account, string pseudo)
    {
        try
        {
            Console.WriteLine($"Votre solde actuel est : {account[pseudo].Solde}");
        }
        catch
        {
            Console.WriteLine("Une erreur est survenue");
            return;
        }
    }
}

class Account
{
    public Dictionary<string, Compte_Bancaire> account = new Dictionary<string, Compte_Bancaire>();
    Banque banque = new Banque();
    private string pseudo;

    public void label()
    {
        Banque banque = new Banque();
        while (true)
        {
            try
            {
                Console.WriteLine("Bienvenue dans votre banque, que voulez-vous faire ?");
                Console.WriteLine("1 - Connexion");
                Console.WriteLine("2 - Inscription");
                Console.WriteLine("3 - Quitter");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        connexion();
                        break;
                    case 2:
                        inscription();
                        break;
                    case 3:
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Une erreur est survenue");
                return;
            }
        }
    }

    public void connexion()
    {
        try
        {
            Console.WriteLine("Veuillez entrer votre pseudo");
            string input_pseudo = Console.ReadLine();

            if (!account.ContainsKey(input_pseudo))
            {
                Console.WriteLine("Aucun utilisateur trouvé");
                return;
            }
            Console.WriteLine("Veuillez entrer votre mot de passe");
            string input_password = Console.ReadLine();
            if (account[input_pseudo].Password != input_password)
            {
                Console.WriteLine("Mot de passe incorrect");
                return;
            }

            banque.label(account, input_pseudo);
        }
        catch
        {
            Console.WriteLine("Une erreur est survenue");
            return;
        }
    }

    public void inscription()
    {
        try
        {
            Console.WriteLine("Veuillez entrer votre nom d'utilisateur (0 - retour)");
            string input_username = Console.ReadLine();
            if (input_username == "0")
            {
                return;
            }
            else if (!isUsername(input_username))
            {
                Console.WriteLine("Le nom d'utilisateur ne respecte pas les conditions");
                return;
            }
            Console.WriteLine("Veuillez entrer le mot de passe (0 - retour)");
            string input_password = Console.ReadLine();
            if (input_password == "0")
            {
                return;
            }
            else if (!isPassword(input_password))
            {
                Console.WriteLine("Le mot de passe ne respecte pas les conditions");
                return;
            }
            Console.WriteLine($"Bienvenue {input_username}");
            Compte_Bancaire compte = new Compte_Bancaire(input_username, input_password);
            account.Add(input_username, compte);
            return;
        }
        catch
        {
            Console.WriteLine("Une erreur est survenue");
        }
    }

    private bool isUsername(string username)
    {
        if (username == null)
        {
            Console.WriteLine("Veuillez remplir le champ");
            return false;
        }
        else if (username.Length > 15 || username.Length < 5)
        {
            Console.WriteLine("Le nom d'utilisateur doit contenir entre 5 et 15 caractères");
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool isPassword(string password)
    {
        if (password == null)
        {
            Console.WriteLine("Veuillez remplir le champ");
            return false;
        }
        else if (password.Length > 20 || password.Length < 8)
        {
            Console.WriteLine("Le mot de passe doit contenir entre 8 et 20 caractères");
            return false;
        }
        else
        {
            return true;
        }
    }
}


class Program
{
    public static void Main()
    {
        Account account = new Account();
        while (true)
        {
            try
            {
                Console.WriteLine("Bienvenue, que voulez vous faire ?");
                Console.WriteLine("1 - Lancer l'application");
                Console.WriteLine("2 - Quitter");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        account.label();
                        break;
                    case 2:
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue : " + ex.ToString());
            }
        }
    }
}
