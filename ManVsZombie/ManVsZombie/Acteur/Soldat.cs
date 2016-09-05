using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acteur
{
    class Soldat : Personnages
    {
        private readonly static string nomBase = "Soldat";
        private readonly static int vieBase = 3;
        private readonly static int niveauBase = 1;
        private readonly static int nbCibleBase = 1;

        private readonly static int degatBase = 1;

        public Soldat(int id)
        {
            Identifiant = id;
            Nom = nomBase;
            VieActuelle = vieBase;
            VieMax = vieBase;
            Niveau = niveauBase;
            NbCible = nbCibleBase;
            AttaqueRestantes = nbCibleBase;
            Degats = degatBase;
            IsVivant = true;
        }

        public Soldat(int vie, int niveau, int nbCible, int degat, int attaquesRestantes)
        {
            VieActuelle = vie;
            VieMax = vie;
            Niveau = niveau;
            AttaqueRestantes = nbCibleBase;
            NbCible = nbCible;
            Degats = degat;
            IsVivant = true;
        }

        /// <summary>
        /// Augmente les points de vie Max de l'unité de 1.
        /// Augmente les points de vie actuelles de l'unité de 1.
        /// Augmente le niveau de l'unité de 1.
        /// Chaque fois que le niveau de l'unité est augmenté de 10, cette dernière gagne 1 attaque.
        /// </summary>
        public void LevelUp()
        {
            VieActuelle++;
            VieMax++;
            Niveau++;
            string messageLevelUp = "Le Soldat" + Identifiant + " a atteint le niveau " + Niveau + " et gagne 1 point de vie.";
            Console.WriteLine(messageLevelUp);
            if ((Niveau - 1) % 10 == 0)
            {
                GainAttaque();
                string messageNbAttaque = "Il possède désormais " + NbCible + "attaques.";
                Console.WriteLine(messageNbAttaque);
            }
        }

        /// <summary>
        /// Le soldat gagne une attaque.
        /// </summary>
        private void GainAttaque()
        {
            NbCible++;
        }
    }
}
