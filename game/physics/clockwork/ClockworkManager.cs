﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages clockwork components
    /// </summary>
    internal class ClockworkManager
    {
        #region Fields and parts
        /// <summary>
        /// Used by PopulateRootLinkageSpriteList()
        /// </summary>
        private HashSet<AbstractLinkage> rootLinkageSpriteList = new HashSet<AbstractLinkage>();

        /// <summary>
        /// Used by PopulateRootLinkageSpriteList()
        /// </summary>
        private HashSet<AbstractLinkage> ignoreList = new HashSet<AbstractLinkage>();

        /// <summary>
        /// Manages pendulum physics
        /// </summary>
        private PendulumManager pendulumManager = new PendulumManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Update visible sprites
        /// </summary>
        /// <param name="toUpdateSpriteList">visible sprites</param>
        /// <param name="timeDelta">time delta</param>
        internal void Update(HashSet<AbstractSprite> toUpdateSpriteList, PlayerSprite playerSprite, double timeDelta)
        {
            PopulateRootLinkageSpriteList(toUpdateSpriteList);

            foreach (AbstractLinkage rootLinkage in rootLinkageSpriteList)
            {
                Update(rootLinkage, playerSprite, timeDelta);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Update current root linkage sprite
        /// </summary>
        /// <param name="rootLinkage">root linkage sprite</param>
        /// <param name="timeDelta">time delta</param>
        private void Update(AbstractLinkage rootLinkage, PlayerSprite playerSprite, double timeDelta)
        {
            if (rootLinkage is Pendulum)
                pendulumManager.Update((Pendulum)rootLinkage, playerSprite, timeDelta);

            if (rootLinkage is AbstractBearing)
                foreach (AbstractLinkage childLinkage in ((AbstractBearing)rootLinkage).ChildList)
                    Update(childLinkage, playerSprite, timeDelta);
        }

        /// <summary>
        /// Populate all sprites that are root linkages
        /// </summary>
        /// <param name="toUpdateSpriteList">list of visible sprites</param>
        private void PopulateRootLinkageSpriteList(HashSet<AbstractSprite> toUpdateSpriteList)
        {
            rootLinkageSpriteList.Clear();
            ignoreList.Clear();

            foreach (AbstractSprite sprite in toUpdateSpriteList)
            {
                if (sprite is AbstractLinkage)
                {
                    AbstractLinkage linkage = (AbstractLinkage)sprite;
                    AbstractLinkage rootParent = GetRootParentNode(linkage);
                    if (rootParent != null)
                    {
                        if (!rootLinkageSpriteList.Contains(rootParent))
                            rootLinkageSpriteList.Add(rootParent);
                    }
                }
            }
        }

        /// <summary>
        /// Get root parent node or null if linkage is in ignore list
        /// </summary>
        /// <param name="linkage">linkage</param>
        /// <returns>root parent node or null if linkage is in ignore list</returns>
        private AbstractLinkage GetRootParentNode(AbstractLinkage linkage)
        {
            if (ignoreList.Contains(linkage))
                return null;

            ignoreList.Add(linkage);

            if (linkage.ParentNode == null)
                return linkage;

            return GetRootParentNode(linkage.ParentNode);
        }
        #endregion

        #region Properties
        public HashSet<AbstractLinkage> RootLinkageSpriteList
        {
            get { return rootLinkageSpriteList; }
        }
        #endregion
    }
}
