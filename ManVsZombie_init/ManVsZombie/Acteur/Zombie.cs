using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acteur
{
    class Zombie : Personnages
    {
        private readonly static string nomBase = "Zombie";
        private readonly static int vieBase = 1;
        private readonly static int niveauBase = 1;
        private readonly static int nbCibleBase = 1;
        private readonly static int degatBase = 1;

        public Zombie(int id)
        {
            Identifiant = id;
            Nom = nomBase;
            VieActuelle = vieBase;
            VieMax = vieBase;
            Niveau = niveauBase;
            NbCible = nbCibleBase;
            Degats = degatBase;
            IsVivant = true;
        }

        public Zombie(int vie, int niveau, int nbCible, int degat)
        {
            VieActuelle = vie;
            VieMax = vie;
            Niveau = niveau;
            NbCible = nbCible;
            Degats = degat;
            IsVivant = true;
        }
    }
}
