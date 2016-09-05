using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acteur

{
    abstract class Personnages : Entite
    {
        #region DeclarationVariable
        protected int _niveau;
        protected int _nbCible;
        protected int _attaqueRestantes;
        protected int _degats;
        #endregion
        #region Proprietes
        public int Niveau
        {
            get
            {
                return _niveau;
            }
            set
            {
                _niveau = value;
            }
        }
        public int AttaqueRestantes
        {
            get
            {
                return _attaqueRestantes;
            }
            set
            {
                _attaqueRestantes = value;
            }
        }
        public int NbCible
        {
            get
            {
                return _nbCible;
            }
            set
            {
                _nbCible = value;
            }
        }
        public int Degats
        {
            get
            {
                return _degats;
            }
            set
            {
                _degats = value;
            }
        }
        #endregion

        /// <summary>
        /// Réduit le nombre d'attaque restante de 1 quand le personnage attaque.
        /// </summary>
        public void Attaque()
        {
            if (AttaqueRestantes > 0)
            {
                AttaqueRestantes--;
            }
        }
    }

}
