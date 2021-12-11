/*
 * PIXI.JS GAME
 */

PIXI.settings.RESOLUTION = window.devicePixelRatio;
PIXI.settings.SORTABLE_CHILDREN = true;

var octopusImages = [
  "./images/Octopuses/frame_01.png",
  "./images/Octopuses/frame_02.png",
  "./images/Octopuses/frame_03.png",
  "./images/Octopuses/frame_04.png",
  "./images/Octopuses/frame_05.png",
  "./images/Octopuses/frame_06.png"
];
var octopusTextureArray = [];
var octopusArray = [[]];
var octopusHeight = 120;
var octopusWidth = 120;


// global variables

var app = new PIXI.Application({
  sharedLoader: true,
  width: window.innerWidth,
  height: window.innerHeight,
  autoDensity: true,
  antialias: true,
  resizeTo: window,
  backgroundColor: 0xF0F0F0
});
document.body.appendChild(app.view);

/* Loading screen */
var load_progress = 0;
var total_files = [
  octopusImages
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
  .add(octopusImages)
  .load(setup);

function octopus(x, y, potentialEnergy) {
  function onClick(e) {

  }

  function reset() {
  }

  function next() {
  }

  function illuminate() {
    if (that.flashed) {
      sprite.filters[0].brightness(1);
    } else {
      sprite.filters[0].brightness(that.energy / 11);
    }
  }

  var sprite = new PIXI.AnimatedSprite(octopusTextureArray);
  sprite.x = x;
  sprite.y = y;
  sprite.vx = 1;
  sprite.interactive = true;
  sprite.on('pointerdown', onClick);
  sprite.animationSpeed = 0.14;
  sprite.play();

  let filter = new PIXI.filters.ColorMatrixFilter();
  filter.brightness(1);
  sprite.filters = [filter];

  var that = {
    sprite: sprite,
    flashed: false,
    energy: potentialEnergy,
    next: next,
    illuminate: illuminate
  };
  return that;
}

function resizeElements() {
  // Perform all actions that require the screen size (place things...)
}

function setup() {
  var init = "5483143223274585471152645561736141336146635738547841675246452176841721688288113448468485545283751526";

  octopusTextureArray.push(PIXI.Texture.from(octopusImages[0]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[1]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[0]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[1]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[0]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[1]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[0]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[1]));
  octopusImages.forEach((i) => {
    octopusTextureArray.push(PIXI.Texture.from(i));
  });
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[3]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[4]));
  octopusTextureArray.push(PIXI.Texture.from(octopusImages[1]));


  // Call the resize util (everything related to the size of the screen)
  app.renderer.on('resize', resizeElements);
  resizeElements();

  for (let row = 0; row < 10; row++) {
    octopusArray[row] = [];
    for (let col = 0; col < 10; col++) {
      let o = octopus(col * octopusWidth + 10, row * octopusHeight + 10, +init[row*10+col]);
      octopusArray[row][col] = o;
      app.stage.addChild(o.sprite);
    }
  }

  // Start the game loop
  app.ticker.add(play);
}

var timeUntilNextFrame = 10;
function play(delta) {
  timeUntilNextFrame -= delta;
  if (timeUntilNextFrame < 0) {
    timeUntilNextFrame = 10;

    //Gain energy
    for (let row = 0; row < 10; row++) {
      for (let col = 0; col < 10; col++) {
        octopusArray[row][col].energy++;
      }
    }

    //Update neighbors
    do {
      for (let row = 0; row < 10; row++) {
        for (let col = 0; col < 10; col++) {
          if (octopusArray[row][col].energy > 9 && !octopusArray[row][col].flashed) {
            octopusArray[row][col].flashed = true;
            for (let acol = col - 1; acol <= col + 1; acol++) {
              for (let arow = row - 1; arow <= row + 1; arow++) {
                if (arow >= 0 && arow < 10 && acol >= 0 && acol < 10
                  && (arow != row || acol != col)) {
                  octopusArray[arow][acol].energy++;
                }
              }
            }
          }
        }
      }
    } while (octopusArray.some(r => r.some(c => c.energy > 9 && !c.flashed)));

    //Deplete energy reserves
    for (let col = 0; col < 10; col++) {
      for (let row = 0; row < 10; row++) {
        octopusArray[row][col].illuminate();
        if (octopusArray[row][col].flashed)
          octopusArray[row][col].energy = 0;
        octopusArray[row][col].flashed = false;
      }
    }
  }

}

/*
* BROWSER EVENTS
*/

window.onload = function () {

};
