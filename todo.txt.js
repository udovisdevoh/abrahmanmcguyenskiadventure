Comportement de koopa pour RiotControl
{
	faire spinner le casque

	ajuster accélération des casques
	
	Helmets must stop when jumped on if they move, or move if they don't
	
	When touching stopped helmet, don't lose energy, must start moving in direction getting pushed
	
	When jumping on helmet or RiotControl, the moving direction will be direction pushed
	
	shells must not be able to climb sharp slopes
	
	shells must make sound when hitting walls
}

Audio
{
	?Remettre son SDL
	regler bug de manque de channel
}

Regler bug de zone de tile hyper repeté

Éliminer ground avec régions cul de sac vertical, éliminer ground avec bosse à pic en loop toujours pareille

Sprite de tampoline

on doit pouvoir passer au travers des pics verticaux en sautant (pas coller dessus)
on doit pouvoir grimper sur les parroies verticales

glisser: doit attaquer monstrers (ne doit plus glisser sur pente trop douce)

fin de level
{
	Boss?
	Fade out noir
	Changement de level
}

life powerup
Miniboss?
?Arme de longe portée si powerup?
?Arme de portée moyenne?, (toujours là)?,  (agrandissable)? light saber? chainsaw?
certains monstres doivent automatiquement sauter par dessus les trous
certains monstres doivent se spawner directement sur le sol (probablement la majorité)
cetrains monstres ne doivent pas avoir de flee mode

Monstres
{
	Stephenson Harpenstein: boss de la fin; Harper Frankenstein

	Police d'escouade anti-émeute
	{
		Comportement d'un koopa, le casque sera comme la carapace
	}
	
	Jesus raptor
	{
		Pieds comme yoshi
	}
	
	Mickey Mouse
	
	Musulman sunith
	{
		Lance des dynamites
	}
	
	Musulman shiite
	{
		Sabre courbé
	}
	
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
	
	Hindu
	{
		Lance des statuettes
	}
	
	Pedo-priest
	{
		En boxers du Canada et avec les jambes à l'air. Pencé par en avant avec les mains sorties comme pour faire un attouchement
	}
	
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
	
	Mormon
	{
		arme: bible
	}
	
	Diseuse de bonne aventure
	{
		lance boule de crystal
	}
	
	Obese (4*4 tile)
	{
		donne des coups de bedaine
		en veston sans chemise avec un shaggy
	}
	
	Trijambiste
	{
		3 jambes
		marche en faisant comme s'il nageait (avec ses bras)
	}
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
		Lorsqu'on saute et qu'on est en train de retomber
		{
			Ça fait des descente de notes
		}
		
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