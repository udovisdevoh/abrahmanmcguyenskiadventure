?remove pink bias?

add texture cache (for all gamestates, use seed and ground index)

Changer nom d'app

lorsque qu'explose alh akbr

?add maximum time delta (will slow down, but physics will be tested properly)?

fix joystick remap

possibilité de transporter une carapace et de la lancer (en haut aussi)

Faire écran de menu
{
	Logo
	
	?si dure trop longtemps, affiche "how to play"?
	
	Menu
	{
		New game
		Load game (?password?) lol
		Graphical options
		{
			Changer résolution
			Fullscreen?
		}
		Setup controller
		{
			Config de joystick
		}
		Audio
		{
			Volume de musique et de sons
		}
		How to play
		{
			Comment jouer (key comb)
			Tous les monstres et leur noms
		}
	}
}

Storyline
{
	Each level must be a planet
	Random name for planet (show while loading), show planet image too (spherical)
	?generate spherical image? use textures?
}

?si tiny: son différent si jump?

Après chaque boss: Un gars d'Anonymous dit: "désolé c'était une marionnette"
{
	Boss de la fin: grosse machine, puis vrai boss, contre sois-même
}

?get frontmost walkable ground
{
	?must not take sprite ground if it's lower than current ground?
}?

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

Test implementation: jump under block: s'il y a un monstre dessus, update sa direction selon angle d'icidence

Faire gestion automatique de la population de sprite
{
	sprite dispatcher must not put anarchy block on a ambiguous height (too low to pass under, but it looks like not)
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



shells must open anarchy blocks and break brick blocks


if top speed
{
	play special sound (beep in loop)
	do special jump (longer jump time)
}


?si invincible -> nombre plus élevé de boules de feux sur l'écran?


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
		
		certain pipes nécéssitent un nombre minimum de seeds pour entrer dedans. (plus gros, look différent), affiche nombre minimum dessus
		Lorsque tu entre dans ces pipe, tu va au niveau suivant (plus de sorte de sprite, certains levels sont plus grands)
		chaque seed d'Alladium à son Hash (genre md5) unique comme ça on ne peut pas pogner le même 2 fois)
	}
}

mettre décorations anarchiques
{
	drapeaux etc
}

add some water, lava etc

Regler bug de zone de tile hyper repeté

Possiblement réglé
{
	Éliminer ground avec régions cul de sac vertical, éliminer ground avec bosse à pic en loop toujours pareille
	on doit pouvoir passer au travers des pics verticaux en sautant (pas coller dessus)
}
?on doit pouvoir grimper sur les parroies verticales

glisser: doit attaquer monstrers (ne doit plus glisser sur pente trop douce)

fin de level
{
	Boss?
	Fade out noir
	Changement de level
}


?on death, change level?
?add key combination for self death (when stucked)?


Miniboss?
certains monstres doivent se spawner directement sur le sol (probablement la majorité)


Monstres
{
	truc qui se comporte comme une plante carnivore (je sais pas quoi encore), possiblement pelle mécanique ou drill de pétrole

	Bosses
	{
		Sarah Palin
		Tom Cruise	
		Ron Hubbard
		Présidents de banque
		Stephenson Harpenstein: boss de la fin: Harper Frankenstein
	}

	Jesus raptor
	{
		améliorer la texture du raptor (+d'ombrages)
		mettre courone d'épine et/ou auréole
		doit tenir une croix dans sa main
		mettre genre de couche
	}
	
	millitaire de l'armé américaine
	
	truc chinois de censure
	
	équivalent de poisson
	
	équivalent de bullet bill
	
	Journaliste
	{
		Marionette (avec fil). Tient un micro
	}
	
	Mormon
	{
		arme: bible
	}
	
	Trijambiste
	{
		3 jambes
		marche en faisant comme s'il nageait (avec ses bras)
	}
	
	Mickey Mouse
	
	Avocat
	
	Juif hassidique (spelling?)
	{
		Lance des sacs d'argent (avec un $)
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
	
	?Hindu
	{
		Lance des statuettes
	}?
	
	?Hippypocrite
	{
		Lance bouteilles de fruitopia en plastique qui pollue
	}?
	
	?Yupi
	{
		Avec sweather attaché dans le cou
		habit de tennis
	}?
	
	?Bobo?
	
	Ronald mcdonalds
	{
		lance des bigmacs, frites, coke, etc
	}
	
	Docteur pharmaceutique
	{
		lance des pilules ou des seringues
	}
	
	Fermier transgénique
	{
		lance des maïs monsanto
	}
	
	?Diseuse de bonne aventure
	{
		lance boule de crystal
	}?
	
	?Obese (4*4 tile)
	{
		donne des coups de bedaine
		en veston sans chemise avec un shaggy
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