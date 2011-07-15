remove unused spatial hasing (to improve performances)

create ruin structure style block dispatcher
fusionner wave et totem (si wave positive: totem)


add SMW style backup item rack

add infinite fireball bonus powerup
add mullet powerup (could be infinite powerup)
?add running powerup (to dash like sonic)

when tiny: jump higher (just enough so head is same height as when jump as not tiny)


add obese kids: shy guy behavior

?fix flashing in water?



dispatch pipes

?teleporter to next level must be on a random ground, not just the top one


skill level must influence
{
	ground wave normalization factor	
	drill types (none, white, black) (in pipes)
}

(probably already done): remove block collision (side and bottom of block) for explosions

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


Menu
{
	Display
	{
		Changer résolution (nécéssite restart)
		Fullscreen?
	}
	Audio
	{
		Volume de musique et des sons
	}
}

Après chaque boss: Un gars d'Anonymous dit: "désolé c'était une marionnette"
{
	Boss de la fin: grosse machine, ?puis vrai boss, contre sois-même?
}

?Maybe increase maxCachedColumnCount = 100 to 1000 and do some prerendering when sprite not walking?

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


?add bouncing notes?
?add pipes or equivalent, must think about it first?
{
	could just be a teleporter to another level. You walk in it, and you go to another level, the game will remember the current level, so you can come back
	//some levels could be inside a cave instead of being outside
	//could maybe add some moutains in the background of some levels
	
	idée cool
	{
		Tu rentre dans un tuyau ou un teleporteur, warp vortex, plante grimpante, ou une porte
		Chaque tuyau à un seed de départ (seed du level dans lequel se trouve le tuyau) et un seed de destination (seed du level généré).
		Ça te téléporte dans un autre level à coté d'un tuyau, téléporteur, warp vortex ou porte dont le seed de destination est inversé par rapport au seed de source du tuyau de provenance
		
		dans les level, tu trouve des seed d'aladdium. Parfois il faut battre un monstre boss pour l'avoir,
		parfois c'est caché
		
		certain pipes nécéssitent un nombre minimum de seeds pour entrer dedans. (plus gros, look différent) (le skill level des level pas encore visités augmente), affiche nombre minimum dessus
		On ne peut utiliser des seeds d'aladdium que si elles proviennent d'un niveau au skill level actuel
		Lorsque tu entre dans ces pipe, tu va au niveau suivant (plus de sorte de sprite, certains levels sont plus grands)
		chaque seed d'Alladium à son Hash (genre md5) unique comme ça on ne peut pas pogner le même 2 fois)
	}
}

mettre décorations anarchiques
{
	drapeaux etc
}


?on doit pouvoir grimper sur les parroies verticales

glisser: doit attaquer monstrers (ne doit plus glisser sur pente trop douce)

fin de level
{
	Boss?
	Fade out noir
	Changement de level
}


?add key combination for self death (when stucked)?


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

Musique générative
{
	Theme pour chaque level
	{
		Tempo de la chanson
		
		Si ternaire ou pas
		
		Kit d'instruments
		
		Mode de la chanson
	}
	
	Ce qui influence la musique
	{
		si le joueur est "dopé", "saoul" ou "rasta", si rasta et en train de voler
		
		si invincible (sera autre chose, genre une sorte de dopage)
		
		si level est fini (genre de musique à la fin des level de smb1)
		
		skill level
		
		si dans l'eau
		
		si dans un pipe
	
		Lorsqu'on saute et qu'on est en train de retomber
		{
			Ça fait des descente de notes
		}
		
		si timer de monster explosif (plus timer approche fin, plus intense, rapide, aigu, dissonant)
		
		Plus on va vitte, plus les notes sont courtes (drum surtout), exmple: position stationnaire: beat de hip hop très peu remplis. Course: Drum and bass
		
		Si on utilise ses armes / pieds / poings: encore plus de percussion
		
		Plus il y a de trous en proximité, aini que de monstres, plus il y a de dissonances
		
		Lorsque des monstres ont étés vaincus récement: + d'accords majeurs
		
		Au dessus d'un trou: plus de silences, avec des pad
		
		Si sol bas, plus de basse, moin d'instruments aigus
		
		Si sol haut et qu'on voit le background: beaucoup d'instruments, comme un gros orchestre
		
		Si on vient de battre un monstre difficile à battre (mini boss): modulation de la tonalité par en haut
	}
}