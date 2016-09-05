using System;
using System.Collections.Generic;
using System.Linq;
using Acteur;

namespace Jeu
{
    class Round
    {
        private static readonly int nbZombieBase = 10;
        private static readonly int nbHumainBase = 3;

        #region Declarations de Variables
        private bool _isFini;
        private List<Personnages> _equipeHumain;
        private List<Personnages> _equipeZombie;
        private Mur _mur;
        #endregion

        #region Proprietes
        public bool IsFini
        {
            get
            {
                return _isFini;
            }
            set
            {
                _isFini = value;
            }
        }

        public List<Personnages> EquipeHumain
        {
            get
            {
                return _equipeHumain;
            }
            set
            {
                _equipeHumain = value;
            }
        }

        public Mur Defense
        {
            get
            {
                return _mur;
            }
            set
            {
                _mur = value;
            }
        }

        public List<Personnages> EquipeZombie
        {
            get
            {
                return _equipeZombie;
            }
            set
            {
                _equipeZombie = value;
            }
        }

        #endregion

        /// <summary>
        /// Déroulement du jeu.
        /// Lance le tour des humains et des zombies successivement jusqu'à qu'il n'y ait plus de soldat.
        /// </summary>
        public Round()
        {
            Init();
            PhaseInitialisation();
            while (!IsGameOver())
            {
                PhaseHumain();
                IsFini = false;
                PhaseZombie();
                NouvelleVagueZombie();
                PhaseTransition();
            }
        }

        #region Initialisation

        /// <summary>
        /// Créations des zombies, des soldats et du mur.
        /// </summary>
        public void Init()
        {
            IsFini = false;
            EquipeHumain = new List<Personnages>();
            EquipeZombie = new List<Personnages>();
            Defense = new Mur();

            for (int i = 0; i < nbZombieBase; ++i)
            {
                Zombie zombie = new Zombie(i + 1);
                EquipeZombie.Add(zombie);
            }

            for (int i = 0; i < nbHumainBase; ++i)
            {
                Soldat soldat = new Soldat(i + 1);
                EquipeHumain.Add(soldat);
            }
        }

        public void PhaseInitialisation()
        {
            Console.WriteLine("---------------- Man VS Zombie ----------------");
            Console.WriteLine("Une vague de " + nbZombieBase + " zombies vient d'apparaître !");
            Console.WriteLine("Seul " + nbHumainBase + " humains peuvent défendre leur château !");
            Console.WriteLine("Appuyez sur une touche pour lancer la simulation.");
            Console.ReadLine();
        }

        #endregion

        #region Gestion des phases
        public void PhaseTransition()
        {
            IsFini = false;
            Console.WriteLine("Appuyez sur une touche pour continuer la simulation.");
            Console.ReadLine();
        }

        /// <summary>
        /// Déroulement du tour des humains.
        /// </summary>
        public void PhaseHumain()
        {
            Soldat soldat;
            resetAttaques(EquipeHumain);
            Console.WriteLine("---------------- Les Humains attaquent ! ----------------");
            while (!IsFini)
            {
                for (int i = 0; i < EquipeHumain.Count; ++i)
                {
                    soldat = (Soldat)EquipeHumain.ElementAt(i);
                    while (soldat.AttaqueRestantes > 0)
                    {
                        if (EquipeZombie.Count == 0)
                        {
                            return;
                        }
                        PhaseAttaque(soldat, EquipeZombie);
                    }
                }
                IsFini = IsPhaseFini(EquipeHumain);
            }
        }

        /// <summary>
        /// Déroulement du tour des Zombies
        /// </summary>
        public void PhaseZombie()
        {
            Zombie zombie;
            resetAttaques(EquipeZombie);
            if (EquipeZombie.Count > 0)
            {
                Console.WriteLine("---------------- Les Zombies attaquent ! ----------------");
            }

            while (!IsFini)
            {
                for (int i = 0; i < EquipeZombie.Count; ++i)
                {
                    Console.WriteLine("----------------");
                    if (IsGameOver())
                    {
                        return;
                    }
                    zombie = (Zombie)EquipeZombie.ElementAt(i);
                    if (Defense.IsVivant)
                    {
                        PersonnagesAttaque(zombie, Defense);
                        LogDeCombat(zombie, Defense);
                        if (!Defense.IsVivant)
                        {
                            return;
                        }
                    }
                    else
                    {
                        PhaseAttaque(zombie, EquipeHumain);
                    }
                }
                IsFini = IsPhaseFini(EquipeZombie);
            }
        }

        /// <summary>
        /// Vérifie que la Phase d'attaque est terminé
        /// </summary>
        /// <param name="personnages"> Liste des personnages effectuant l'attaque</param>
        /// <returns></returns>
        public bool IsPhaseFini(List<Personnages> personnages)
        {
            foreach (Personnages personnage in personnages)
            {
                if (personnage.AttaqueRestantes >= 1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Vérifie que la partie n'est pas fini c'est à dire qu'il reste encore des soldats
        /// </summary>
        /// <returns></returns>
        public bool IsGameOver()
        {
            if (EquipeHumain.Count > 0)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Gestion d'attaque

        /// <summary>
        /// L'attaquant attaque une cible selectionné aléatoirement.
        /// Si l'attaquant est un humain, il montra de niveau si la cible meurt.
        /// </summary>
        /// <param name="attaquant">Personnage effectuant l'attaque</param>
        /// <param name="EquipeAttaquee">Equipe subissant l'attaque</param>
        public void PhaseAttaque(Personnages attaquant, List<Personnages> EquipeAttaquee)
        {
            Random rnd = new Random();
            int numPersonnage = rnd.Next(0, EquipeAttaquee.Count);
            Personnages victime = EquipeAttaquee.ElementAt(numPersonnage);
            if (attaquant.AttaqueRestantes > 0)
            {
                PersonnagesAttaque(attaquant, victime);
                LogDeCombat(attaquant, victime);
                if (!victime.IsVivant)
                {
                    if (attaquant.Nom == "Soldat")
                    {
                        Soldat soldat = (Soldat)attaquant;
                        soldat.LevelUp();
                    }
                    Mort(EquipeAttaquee, numPersonnage);
                }
            }
        }

        /// <summary>
        /// Effectue l'attaque d'un personnage sur une entité.
        /// </summary>
        /// <param name="personnages"> Personnage effectuant l'attaque </param>
        /// <param name="entite"> Entite recevant les dommages</param>
        public void PersonnagesAttaque(Personnages personnages, Entite entite)
        {
            personnages.Attaque();
            entite.SubitDegats(personnages.Degats);
        }

        /// <summary>
        /// Retire le personnage ayant été tué.
        /// Ecrit dans le console le message concernant la mort du personnage
        /// Si tous les personnages ont été tués, un message est écrit en console.
        /// </summary>
        /// <param name="personnage">Personnage tué.</param>
        /// <param name="numero">Numéro dans la liste du personnage.</param>
        public void Mort(List<Personnages> personnage, int numero)
        {
            Personnages perso = personnage.ElementAt(numero);
            personnage.RemoveAt(numero);
            Console.WriteLine("Le " + perso.Nom + " " + perso.Identifiant + " a été tué.");
            Console.WriteLine("----------------");

            if (personnage.Count == 0)
            {
                Console.WriteLine("Tout les " + perso.Nom + "s ont été tué.");
                Console.WriteLine("----------------");
            }
        }

        /// <summary>
        /// Réinitialise les attaques restantes d'une équipe.
        /// </summary>
        /// <param name="personnages">Equipe bénéficiant d'une réinitialisation d'attaque</param>
        public void resetAttaques(List<Personnages> personnages)
        {
            foreach (Personnages perso in personnages)
            {
                if (perso.IsVivant)
                {
                    perso.AttaqueRestantes = perso.NbCible;
                }
            }
        }

        #endregion

        #region Gestion des messages

        /// <summary>
        /// Ecrit les messages relatifs au combat dans la console
        /// </summary>
        /// <param name="personnage">Personnage effectuant l'attaque</param>
        /// <param name="entite">Entité subissant les dommages</param>
        public void LogDeCombat(Personnages personnage, Entite entite)
        {
            string messageDegats = "Le " + entite.Nom + " " + entite.Identifiant + " a subit " +
                personnage.Degats + " de dégat du " + personnage.Nom + " " + personnage.Identifiant;

            string messageVie = "Il reste " + entite.VieActuelle + " point de vie a l'entite " + entite.Nom +
                " " + entite.Identifiant;

            Console.WriteLine(messageDegats);
            Console.WriteLine(messageVie);
        }

        #endregion

        #region Gestion des vagues

        /// <summary>
        /// Génére une nouvelle vague de zombie quand tous les zombies ont été tué.
        /// </summary>
        public void NouvelleVagueZombie()
        {
            if (EquipeZombie.Count == 0)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Une nouvelle vague de " + nbZombieBase + " vient d'arriver !");
                Console.WriteLine("----------------");
                for (int i = 0; i < nbZombieBase; ++i)
                {
                    Zombie zombie = new Zombie(i);
                    EquipeZombie.Add(zombie);
                }
            }
        }

        #endregion
    }
}
