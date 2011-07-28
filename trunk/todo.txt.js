si ground transparent
{
	soit on peut traverser les murs 
	soit on voit la délimitation des murs (une genre de ligne
}

?create ruin structure style block dispatcher
fusionner wave et totem (si wave positive: totem)


?add SMW style backup item rack?

add infinite fireball bonus powerup
add mullet powerup (could be infinite powerup)
?add running powerup (to dash like sonic)

when tiny: jump higher (just enough so head is same height as when jump as not tiny)


add obese kids: shy guy behavior

fix flashing in water


Salvia powerup
{
	Bonus "3D" style lotus 3
}


?teleporter to next level must be on a random ground, not just the top one


?skill level must influence ground wave normalization factor

(probably already done): remove block collision (side and bottom of block) for explosions

Bonus "achetable" gratuitement
{
	Réduire résistance à l'alcool (invincibilité dure plus longtemps)
	Réduire résistance à la mescaline (plus de boules de feu)
	Meilleurs cheveux de rasta (tomber plus lentement lorsqu'on plane)
	Meilleur nage
	Sauter plus haut
	Courir plus vitte
	Meilleure résistance aux monstres
	Castor gruge plus large
	Punch/kick plus fort
	Punch/kick plus vitte
	Punch/kick plus loin
}

Sprite dispatcher
{
	Blocks
	{
		Wave of horizontal segments
		{
			Vertical Waves to dispatch block segments (could be sineish or not)
			Wave for block segment width
			Wave for distance between segments
			Voir SMB1 1-1
		}
		
		Vertical structure
		{
			Wave for distance between horizontal segments
			Wave for horizontal segment width
			Voir SMB1 5-2, (flat on ground: see SMB3 W2-Quicksand)
		}
		
		Unbreakable block ground wave
		{
			Wave of unbreakable blocks going from ground up
			Voir SMB1 6-1
			
			-Also add optional wave qui va creuser la structure de block par en dessous
			Voir SMB1 6-1
			
			-Also add option wave qui va séparer la structure en colonnes
			Voir SMB1 1-2
			
			(for breakable version, see SMB3 2-3
		}
		
		Unbreakable block roof wave
		{
			Could also be a Ground (wave ground), but on top (new feature, inverted ground on top)
			
			Voir SMB3 W1Fortress
		}
		
		continuer à checker level de SMB3 à partir de W3
	}
	
	SMB3 6-10: great wall
}

nuages qu'on peut marcher dessus

équivalent de plante grimpante

fix add texture cache transparency bug

Changer nom d'app

create persistant config
{
	Save sound and music volues
	Joystick buttons
	screen resolution
	whether it is fullscreen
}

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


faire des grands trous
si grand trou, mettre plateforme qui suit fonction mathématique ou onde (aussi trucs qui montent en assenceur comme dans smb1 level w1 l2


faire sprites volants
faire sprites volants qui suivent des path de wave ou de fonction mathématiques (ronds, etc)
faire plateformes qui suivent des path de wave ou de fonction mathématiques (ronds, etc)
si rond, mettre sprite au centre et bras qui font tourner
afficher path, ou pas, dépendant de la situation
plateformes de largeur diverse



?if top speed
{
	play special sound (beep in loop)
	do special jump (longer jump time)
}?

some levels could be inside a cave instead of being outside
could maybe add some moutains in the background of some levels
some water

?add bouncing notes?


?on doit pouvoir grimper sur les parroies verticales

glisser: doit attaquer monstrers (ne doit plus glisser sur pente trop douce)


?add key combination for self death (when stucked)?

Boss?
Miniboss?


Monstres
{
	?Comportement de shy guy?
	{
		?Enfant obese (4*4 tile)
		{
			donne des coups de bedaine
		}?
	}
	
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
	
	équivalent de bow-wow (chien en laisse, rond noir avec des grosses dents)
	
	millitaire de l'armé américaine
	
	truc chinois de censure
	
	équivalent de poisson sautant/volant
	{
		?genre de journaliste qui tient un micro et qui essaie de te pogner au vol)
	}
	
	équivalent de boo
	{
		?Avocat
	}
	
	Rcmp
	
	Trijambiste
	{
		3 jambes
		marche en faisant comme s'il nageait (avec ses bras)
	}
	
	Texan
	{
		Chapeau de cowboy
		Ceinture à boucle
		bottes de cowboy
		Arme: une carabine
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