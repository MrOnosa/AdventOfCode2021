/*
 * PIXI.JS GAME
 * https://adventofcode.com/2021/day/11
 */

PIXI.settings.RESOLUTION = window.devicePixelRatio;
PIXI.settings.SORTABLE_CHILDREN = true;
//                                                         ,
let dumbosImages = [                //   (                /o
  "./images/Octopuses/frame_01.png",//    \,           .o;o'           ,o'o'o.
  "./images/Octopuses/frame_02.png",//   /\o;o,,,,,;o;o;''         _,-o,-'''-o:o.
  "./images/Octopuses/frame_03.png",//    \    'o'o'o''         _,-'o,o'         o
  "./images/Octopuses/frame_04.png",//  .o \.              __,-o o,o'
  "./images/Octopuses/frame_05.png",//  | o o'-..____,,-o'o o_o-'
  "./images/Octopuses/frame_06.png" //     'o.o_o_o_o,o--''
];
let scoreboard;
let dumbosTextureArray = [];
let dumbos = [[]];

// global variables
let speed = 12; // Lower is faster.
let score = 0;
let freezeScore = false;

let app = new PIXI.Application({
  sharedLoader: true,
  width: window.innerWidth,
  height: window.innerHeight,
  autoDensity: true,
  antialias: true,
  resizeTo: window,
  backgroundColor: 0xD0D0FF
});
document.body.appendChild(app.view);

/* Loading screen */
let load_progress = 0;
let total_files = [
  dumbosImages
].map((i) => i.length).reduce((a, b) => a + b, 0);
PIXI.Loader.shared.onProgress.add(() => {
  load_progress += 1;
  document.getElementById("loadingstatus").value = load_progress / total_files * 100;
});
PIXI.Loader.shared.onComplete.add(() => {
  let elem = document.getElementById("loading");
  elem.parentNode.removeChild(elem);
});

/* Load files */
PIXI.Loader.shared
  .add(dumbosImages)
  .load(setup);

function octopus(potentialEnergy) {
  function onClick(e) {
    let filter = new PIXI.filters.ColorMatrixFilter();
    filter.hue(Math.floor(Math.random() * 360));
    sprite.filters[1] = filter;
  }

  function illuminate() {
    if (that.flashed) {
      sprite.filters[0].brightness(1);
    } else {
      sprite.filters[0].brightness(that.energy / 11);
    }
  }

  let sprite = new PIXI.AnimatedSprite(dumbosTextureArray);
  sprite.interactive = true;
  sprite.on('pointerdown', onClick);
  sprite.animationSpeed = 0.14;
  sprite.play();

  let filter = new PIXI.filters.ColorMatrixFilter();
  filter.brightness(1);
  sprite.filters = [filter];

  let that = {
    sprite: sprite,
    flashed: false,
    energy: potentialEnergy,
    illuminate: illuminate
  };
  illuminate();
  return that;
}

function resizeElements() {
  // Perform all actions that require the screen size (place things...)
  const playableWidth = app.screen.width - 10;
  const playableHeight = app.screen.height - 10;
  let paddingX = 0;
  let paddingY = 0;
  scoreboard.position.set(playableWidth, playableHeight);
  let r;
  if (playableWidth > playableHeight) {
    r = playableHeight / 10;
    paddingX = (playableWidth - r * 10) / 2
  } else {
    r = playableWidth / 10;
    paddingY = (playableHeight - r * 10) / 2
  }
  for (let row = 0; row < 10; row++) {
    for (let col = 0; col < 10; col++) {
      dumbos[row][col].sprite.width = r;
      dumbos[row][col].sprite.height = r;
      dumbos[row][col].sprite.x = (col * r + 10 + paddingX);
      dumbos[row][col].sprite.y = (row * r + 10 + paddingY);
    }
  }
}

function setup() {
  let init = "5483143223274585471152645561736141336146635738547841675246452176841721688288113448468485545283751526";
  //init = "8624818384372547334366183418274573826616835732214268463583177286886112813868511761611242673848415383"; //MrOnosa's Puzzle Input
  let style = new PIXI.TextStyle({
    fontFamily: "Arial",                     //                        ___                                                                                                                                          
    fontSize: 36,                            //                     .-'   `'.                                                                                                                                
    fill: "white",                           //                    /         \                                                                                                                                 
    dropShadow: true,                        //                    |         ;                                                                                                                                    
    dropShadowColor: "#000000",              //               Sup  |         |           ___.--,                                                                                                                                         
    dropShadowBlur: 2,                       //           _.._     |0) ~ (0) |    _.---'`__.-( (_.                                                                                                                                   
    dropShadowAngle: Math.PI / 6,            //    __.--'`_.. '.__.\    '--. \_.-' ,.--'`     `""`                                                                                                                                                
    dropShadowDistance: 3,                   //   ( ,.--'`   ',__ /./;   ;, '.__.'`    __                                                                                                                                         
  });                                        //   _`) )  .---.__.' / |   |\   \__..--""  """--.,_                                                                                                                                                                                                  
  scoreboard = new PIXI.Text("", style);     //  `---' .'.''-._.-'`_./  /\ '.  \ _.-~~~````~~~-._`-.__.'                                                                                                                                                       
  scoreboard.zIndex = 2;                     //        | |  .' _.-' |  |  \  \  '.               `~---`                                                                                                                                       
  scoreboard.anchor.x = 1;                   //         \ \/ .'     \  \   '. '-._)                                                                                                                                         
  scoreboard.anchor.y = 1;                   //          \/ /        \  \    `=.__`~-.                                                                                                                                         
  setScore();                                //          / /\         `) )    / / `"".`\                                                                                                                            
  app.stage.addChild(scoreboard);            //    , _.-'.'\ \        / /    ( (     / /                                                                                                                                                
  //                                               `--~`   ) )    .-'.'      '.'.  | (
  //                                                      (/`    ( (`          ) )  '-;    
  //                                                       `      '-;         (-'                                                                                                                                                                                          
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[0]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[1]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[0]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[1]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[0]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[1]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[0]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[1]));
  dumbosImages.forEach((i) => {
    dumbosTextureArray.push(PIXI.Texture.from(i));
  });
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[3]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[4]));
  dumbosTextureArray.push(PIXI.Texture.from(dumbosImages[1]));

  // Init dumbo octopuses 
  for (let row = 0; row < 10; row++) {
    dumbos[row] = [];
    for (let col = 0; col < 10; col++) {
      let o = octopus(+init[row * 10 + col]);
      dumbos[row][col] = o;
      app.stage.addChild(o.sprite);
    }
  }
  // Call the resize util (everything related to the size of the screen)
  app.renderer.on('resize', resizeElements);
  resizeElements();

  // Start the game loop
  app.ticker.add(play);
}

let timeUntilNextFrame = speed;
function play(delta) {
  timeUntilNextFrame -= delta;
  if (timeUntilNextFrame < 0) {
    timeUntilNextFrame = speed;

    // Gain energy
    for (let row = 0; row < 10; row++) {
      for (let col = 0; col < 10; col++) {
        dumbos[row][col].energy++;
      }
    }

    // Update neighbors
    do {
      {
        {
          for (let row = 0; row < 10; row++) {
            for (let col = 0; col < 10; col++) {
              if (dumbos[row][col].energy > 9 && !dumbos[row][col].flashed) {
                dumbos[row][col].flashed = true;
                for (let acol = col - 1; acol <= col + 1; acol++) {
                  for (let arow = row - 1; arow <= row + 1; arow++) {
                    if (arow >= 0 && arow < 10 && acol >= 0 && acol < 10
                      && (arow != row || acol != col)) {
                      dumbos[arow][acol].energy++;
                    }//                .---.         ,,
                  }  //     ,,        /     \   Hi  ;,,'  
                }    //    ;, ;      (  o  o )      ; ;
              }      //      ;,';,,,  \  \/ /      ,; ;
            }        //   ,,,  ;,,,,;;,`   '-,;'''',,,'
          }          //  ;,, ;,, ,,,,   ,;  ,,,'';;,,;''';
        }            //     ;,,,;    ~~'  '';,,''',,;''''
      }              //                        '''
    } while (dumbos.some(r => r.some(c => c.energy > 9 && !c.flashed)));

    if (!freezeScore) {
      freezeScore = dumbos.every(r => r.every(c => c.flashed));
      score++;
    }

    // Deplete energy reserves
    for (let col = 0; col < 10; col++) {
      for (let row = 0; row < 10; row++) {
        dumbos[row][col].illuminate();
        if (dumbos[row][col].flashed)
          dumbos[row][col].energy = 0;
        dumbos[row][col].flashed = false;
      }
    }
    setScore();
  }
}

function setScore() {
  scoreboard.text = score.toString();
  if (freezeScore) {
    scoreboard.style.fill = "yellow"
  }
}