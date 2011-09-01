arbres
{
	sur map et dans level
	(générer arbre pour une planete)
	si beaucoup d'arbres sur map, beaucoup d'arbres dans planete (peut être triangle waves sur un layer de parallax)
}

?si pese down et jump, lâche liane et ne la repogne pas

bug: si ninja puis new game: ninja et tiny

meta-monde
{
	faire sprite d'abrahman sur la map (tous les grandeurs, powerups et avec castor)

	générer map
	{
		zones concentriques ou adjacentes
		{
			map avec zones concentriques ou adjacentes.
			chaque level est une porte pour sortire d'une zone concentrique
			si level fini, on peut marcher sur le sprite de level sans collision
		}
	
		générer terrain avec perlin noise
		{
			eau, montagnes etc
		}
		
		générer emplacement des levels et des "places de bonus"
		{
			icone de level, tenir compte de ces infos
			{
				si toit (icône différente)
				si eau ?(doit être sur eau sur map)
				si mur ?(doit être près d'une dénivellation prononcée)
			}
			
			icone de place de bonus (voir Bonus "achetable" gratuitement)
		}
		
		générer routes entre les levels en utilisant aStar
		{
			générer un chemin unique du 1er au dernier level (chemin le plus court, ne passe pas nécéssairement par tous les levels)
			plugger les levels restants pour faire autres chemins courts (début à la fin) qui passent par d'autres levels
		}
	}
	
	une planete par épisode
	
	musique lorsque sur map de meta-monde
	
	map de la planete (avec terrain et perlin noise)
	
	Bonus "achetable" gratuitement
	{
		Réduire résistance à l'alcool (invincibilité dure plus longtemps)
		Réduire résistance à la mescaline (plus de boules de feu et de shuriken)
		Meilleurs cheveux de rasta (tomber plus lentement lorsqu'on plane)
		voler comme rasta lorsque ninja
		Meilleur nage
		Sauter plus haut
		Courir plus vitte
		Meilleure résistance aux monstres
		Castor gruge plus large
		Punch/kick/katana plus fort
		Punch/kick/katana plus vitte
		Punch/kick/katana plus loin (modifier offset pour réfleter range)
	}
	
	trucs spéciaux de manipulation du temps
	{
		?ralentir le temps lorsque court vite (time delta réduit pour tout sauf player)?
		?inverser le temps (time delta négatif)?
	}
}

à la fin d'un level: toujours exponentiel (haut ou bas) pour éviter portails inaccessible

si rasta: musique spéciale

ninja
{	
	doit être facile de quitter la corde même si en haut (press left and right)

	?si déjà jump: possibilité de double jump?->?aussi: dire dans howto?
	
	?don't fall when having side collision
}

faire video youtube promotionnel
demander à AVGN (et autres personnes du genre) de faire review du jeux

optimize cache (add some static sprites on level viewer's cache) clouds, blocks, bricks, trampolines, vines etc

?remove stuff like: getfartestwalkingdistancenocollision?


faire des grands trous
si grand trou, mettre plateforme qui suit fonction mathématique ou onde (aussi trucs qui montent en assenceur comme dans smb1 level w1 l2

faire sprites volants
faire sprites volants qui suivent des path de wave ou de fonction mathématiques (ronds, etc)
faire plateformes qui suivent des path de wave ou de fonction mathématiques (ronds, etc)
si rond, mettre sprite au centre et bras qui font tourner
afficher path, ou pas, dépendant de la situation

plateformes de largeur diverse




some trampolines must be on blocks (and clouds)



add great walls that can only be crossed by vine







column parallax







could maybe add some parallax moutains in the background of some levels


musique
{
	wave pour raisonnance
	wave pour filter
	wave pour force du delay ?(si threshold atteint, fait delay)?
	wave pour durée du delay
	fave pour pitch du delay (octave)
}





?create ruin structure style block dispatcher
fusionner wave et totem (si wave positive: totem)


?add SMW style backup item rack?

add infinite fireball bonus powerup
add mullet powerup (could be infinite powerup)
?add running powerup (to dash like sonic)





fix flashing in water


Salvia powerup
{
	Bonus "3D" style lotus 3
}


?teleporter to next level must be on a random ground, not just the top one






fix add texture cache transparency bug

Après chaque boss: Un gars d'Anonymous dit: "désolé c'était une marionnette"
{
	Boss de la fin: grosse machine, ?puis vrai boss, contre sois-même?
}

fix fire balls
{
	must show explode image when touching impassable sprite or monster
	ne doivent pas être affectées par les pentes à monter
}

?compteur de "music note" dans le hud? si 100, ajoute une vie et reset compteur?? augmente score lorsque touche music note? affiche score dans hud?
{
	?Note de musique: influencera la musique générative (jouera une note qui fit bien dans la tune au moment précis où elle est jouée)?
}

collision detection
{
	Make sure there are no duplicate of horizontal collision test
}





?if top speed
{
	play special sound (beep in loop)
	do special jump (longer jump time)
}?





?add bouncing notes?


?on doit pouvoir grimper sur les parroies verticales

glisser: doit attaquer monstrers (ne doit plus glisser sur pente trop douce)


?add key combination for self death (when stucked)?


Boss?
Miniboss?


Monstres
{
	équivalent de bow-wow (chien en laisse, rond noir avec des grosses dents)
	{
		Texan
		{
			Chapeau de cowboy
			Ceinture à boucle
			bottes de cowboy
			Arme: une carabine
		}
	}
	
	Rcmp
	
	Bosses
	{
		Tom Cruise
		John Travolta
		Le pape
		Sarah Palin
		Ron Hubbard
		Présidents de banque
		Stephenson Harpenstein: boss de la fin: Harper Frankenstein
	}
	
	millitaire de l'armé américaine
	
	truc chinois de censure
	
	équivalent de poisson sautant/volant
	{
		?genre de journaliste qui tient un micro et qui essaie de te pogner au vol)
	}
	
	Trijambiste
	{
		3 jambes
		marche en faisant comme s'il nageait (avec ses bras)
	}
	
	T Party
	{
		Tea party trailer park guy
	}
	
	?Yupi
	{
		Avec sweather attaché dans le cou
		habit de tennis
	}?
}