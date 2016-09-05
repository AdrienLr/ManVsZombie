using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acteur
{
    abstract class Entite
    {
        #region Declaration de Variables
        protected int _identifiant;
        protected string _nom;
        protected int _vieActuelle;
        protected int _vieMax;
        protected bool _isVivant;
        #endregion
        #region Proprietes
        public string Nom
        {
            get
            {
                return _nom;
            }
            set
            {
                _nom = value;
            }
        }

        public int Identifiant
        {
            get
            {
                return _identifiant;
            }
            set
            {
                _identifiant = value;
            }
        }

        public int VieActuelle
        {
            get
            {
                return _vieActuelle;
            }
            set
            {
                _vieActuelle = value;
            }
        }

        public int VieMax
        {
            get
            {
                return _vieMax;
            }
            set
            {
                _vieMax = value;
            }
        }

        public bool IsVivant
        {
            get
            {
                return _isVivant;
            }
            set
            {
                _isVivant = value;
            }
        }
        #endregion

        /// <summary>
        /// Retire des Point de vie à l'entité en fonction des dégats reçus.
        /// Si l'entité atteint 0 point de vie, elle sera considéré comme détruite.
        /// </summary>
        /// <param name="degats">Dégats reçus par l'entité</param>
        public void SubitDegats(int degats)
        {
            if (IsVivant)
            {
                VieActuelle = VieActuelle - degats;
                if (VieActuelle == 0)
                {
                    IsVivant = false;
                }
            }
        }
    }
}
