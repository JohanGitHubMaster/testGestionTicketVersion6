guide de lancement des différent tests :
-Base de données utilisé "SQLSERVER"
-Configuration de la base de données 
	configuration GestionTicket :
		-modifier dans appsettings.json la connexion au serveur correspondant
		-exécuter dans le projet GestionTicket la commande  "update-database" pour insérer la base de données dans le Sql 
		-insérer les données de test généré par le site "mokaroo" des utilisateurs et des tickets en exécutant la requête dans "Data.sql" une seul fois
 
test des API :
	-lancer le projet "GestionTicket" 
	-Un swagger s'affiche : 
 		-exécuter en premier le lien api /GetToken/{user} => un utilisateur de test admin "btopham0" pour accéder 		a tous les endpoints 
		un utilisateur de test user simple "rcoppoa" qui a des accès limités 
		copier le token dans Authorize en haut de la page et exécuter les différents EndPoints

test de l'application Web MVC 
-laisser lancer le projet "GestionTicket"
-lancer dans une nouvelle "Visual studio" ou "vsCode" le projet "GestionTicket.Web"
-tester

les tests unitaires sont présents dans le projet  "GestionTicket.test"