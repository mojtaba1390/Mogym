
var app = app || { common: { vars: {}, fn: {} } };
app.home = {};


/*==========================================
=            Defining Variables            =
==========================================*/

app.home.vars = {

};


/*==================================
=            Prototypes            =
==================================*/



/*==========================================
=            Defining Functions            =
==========================================*/

app.home.fn =
{
	beforeLoadingFunction: function () {
		app.home.fn.factsSectionAnimations().init();
		app.common.fn.marketsSectionAnimations().init();
		app.home.fn.circlesHandler.init();
		app.home.fn.fadingTriangles().init();
	},

	afterLoadingFunction: function () {
		app.home.fn.factsSectionAnimations().render();
		app.common.fn.marketsSectionAnimations().render();
		app.common.fn.articlesAnimationHandler().render();
		/*app.home.fn.circlesHandler.play();*/
	},

	factsSectionAnimations: function () {
		var preSelectors = {

			ta: '.section-facts ',

		},

			init = function () {

				var elementsToBeHiddenOnStart = [

					// Timeline A
					preSelectors.ta + '.fact-item',

				];

				for (var i in elementsToBeHiddenOnStart) {
					app.common.fn.makeInvisible([elementsToBeHiddenOnStart[i]]);
				}

			},

			create = function () {

				// Timeline A
				var timelineA = new TimelineMax({
					// paused:true
					align: 'sequence',
				});

				if ($j(preSelectors.ta + '.fact-item').length) {

					timelineA.staggerFromTo($j(preSelectors.ta + '.fact-item'), 0.6, {
						y: 30,
						autoAlpha: 0,
					}, {
						y: 0,
						autoAlpha: 1,
						onStart: function (tween) {
							var el = tween.target,
								$el = $j(el);

							app.common.fn.makeVisible(el);

							if ($el.length && $el.find('.progress-bar').length && $el.find('.progress-bar').data('progressbar')) {
								var progressbar = $el.find('.progress-bar').data('progressbar'),
									percentage = $el.find('.progress-bar').data('progressPercentage');

								progressbar.animate(percentage / 100);
							}
						},
						onStartParams: ['{self}'],
					}, 0.3);

				}

				if ($j(preSelectors.ta + '.button-container').length) {

					timelineA.fromTo($j(preSelectors.ta + '.button-container'), 0.3, {
						scale: 0,
						autoAlpha: 0,
					}, {
						scale: 1,
						autoAlpha: 1,
					}, '-=0.3');

				}

				return {
					a: timelineA,
				};

			},

			render = function () {

				var controller = null,
					isMobile = false,
					body = $j('body').get(0),
					timelines = create(),
					makeScenes = function () {

						if (controller) controller.destroy();

						controller = new ScrollMagic.Controller();
						isMobile = app.common.fn.isMobile() ? true : false;

						new ScrollMagic
							.Scene({
								reverse: false,
								triggerElement: isMobile ? body : preSelectors.ta,
								duration: isMobile ? 0 : 0
							})
							.setTween(timelines.a)
							.addTo(controller);

					};

				makeScenes();

				if (!app.common.fn.isMobile())
					$j(window).on('resize', makeScenes);

			};

		return {

			init: init,
			create: create,
			render: render

		};

	},

	circlesHandler:
	{
		timeline: [],

		introTimeline: null,

		duration: 15,

		circlesContainer: function () {
			return $j('.circles-container');
		},

		container: function () {
			return this.circlesContainer().find('.pack-nodes-container');
		},

		items: function () {
			return this.container().find('.node-item');
		},

		createPath: function (radius) {
			var path = 'M 0,' + radius + ' m 0,-' + radius + '' +
				' a ' + radius + ',' + radius + ' 0 1 1 0,' + 2 * radius +
				' a ' + radius + ',' + radius + ' 0 1 1 0,-' + 2 * radius;

			return path;
		},

		createBezier: function (radius) {
			var path = this.createPath(radius),
				bezier = MorphSVGPlugin.pathDataToBezier(path);

			return bezier;
		},

		positionOnCircle: function () {
			var that = this,
				total = this.items().length,
				containerRadius = Math.round(this.container().width() / 2),
				angle = (2 * Math.PI) / total;

			this.items().each(function (index, el) {
				var $el = $j(el),
					index = 0,
					circleRadius = Math.round($el.width()),
					x = Math.sin(angle * index) * containerRadius + containerRadius - circleRadius / 2,
					y = -Math.cos(angle * index) * containerRadius + containerRadius - circleRadius / 2;

				TweenMax.set(el, {
					top: y,
					left: x
				});
			});
		},

		init: function () {
			var that = this,
				total = this.items().length,
				bezier = this.createBezier(this.container().width() / 2),
				angle = (2 * Math.PI) / total;

			for (var i = 0; i < that.timeline.length; i++) {
				that.timeline[i].kill();
			}

			this.timeline = [];

			this.items().each(function (index, el) {
				var $el = $j(el);

				that.timeline[index] = new TimelineMax({
					paused: true,
					align: 'sequence',
					repeat: -1
				});

				that.timeline[index].to(el, that.duration, {
					ease: Linear.easeNone,
					bezier: {
						type: 'cubic',
						values: bezier
					},
				});

				// initial position
				that.timeline[index].seek(index * that.duration / total);
			});

			that.introAnimation();

			this.pauseAnimation();
		},

		introAnimation: function () {
			var that = this,
				total = this.items().length,
				angle = (2 * Math.PI) / total;

			if (that.introTimeline) { that.introTimeline.pause(); }

			that.introTimeline = new TimelineMax({
				paused: true,
				align: 'sequence'
			});

			this.items().each(function (index, el) {
				var $el = $j(el);

				that.introTimeline.fromTo($el.find('.node-item-inner'), 0.7, {
					x: -200 * Math.sin(index * angle),
					y: 200 * Math.cos(index * angle),
					autoAlpha: 0,
				}, {
					x: 0,
					y: 0,
					autoAlpha: 1,
					ease: Back.easeOut.config(1.7)
				}, index ? '-=0.5' : '');
			});
		},

	/*	play: function () {
			var that = this,
				controller = null,
				scene = null,
				isMobile = false,
				body = $j('body').get(0),
				makeScenes = function () {
					if (controller) {
						controller.destroy();

						that.init();
					}

					controller = new ScrollMagic.Controller();
					isMobile = app.common.fn.isMobile() ? true : false;

					scene = new ScrollMagic
						.Scene({
							reverse: false,
							triggerElement: isMobile ? body : that.container().get(0),
							duration: isMobile ? 0 : 0
						})
						// .setTween ( timelines )
						.on('start', function () {
							for (var i = 0; i < that.timeline.length; i++) {
								that.timeline[i].play();
							}
							that.introTimeline.play();
						})
						.addTo(controller);

				};

			makeScenes();

			// if ( ! app.common.fn.isMobile() )
			$j(window).on('resize', makeScenes);
		},*/

		pauseAnimation: function () {
			var that = this;

			this.circlesContainer().find('.pack-nodes-container, .projects-slider').off('mouseenter.circle');
			this.circlesContainer().find('.pack-nodes-container, .projects-slider').off('mouseleave.circle');

			this.circlesContainer().find('.pack-nodes-container, .projects-slider').on('mouseenter.circle', function (e) {
				for (var i = 0; i < that.timeline.length; i++) {
					that.timeline[i].pause();
				}
			});

			this.circlesContainer().find('.pack-nodes-container, .projects-slider').on('mouseleave.circle', function (e) {
				for (var i = 0; i < that.timeline.length; i++) {
					that.timeline[i].play();
				}
			});
		}
	},

	fadingTriangles: function () {
		var c,
			canvas,
			$canvas,
			container = $j('.triangles-bg'),
			size = null,
			counter = 0,
			width = 40,
			mouse_x = -1,
			mouse_y = -1,
			fps = 100,
			init = true,
			now = null,
			then = Date.now(),
			interval = 10000 / fps,
			matrix = [],
			init = function () {
				if (container.length) {
					resizeCanvas();

					window.addEventListener('resize', resizeCanvas, false);
					window.addEventListener('mousemove', trackMouse, false);
					window.addEventListener('touchmove', trackMouse, false);
				}
			},
			createCanvas = function () {
				if (container.find('canvas').length) {
					container.find('canvas').remove();
				}

				$canvas = $j('<canvas/>');
				c = $canvas.get(0).getContext('2d');
				canvas = $canvas.get(0);

				$canvas.appendTo(container);
			}
		resizeCanvas = function () {
			createCanvas();

			matrix = [];

			canvas.width = container.width();
			canvas.height = container.height();
			size = Math.ceil(canvas.width / width);
			counter = 0;
			for (var x = 0; x < width * 2; x++) {
				for (var y = 0; y < Math.ceil(canvas.height / size); y++) {
					matrix.push({
						x: x,
						y: y,
						_x: x * size / 2,
						_y: y * size
					});
				}
			}
			init = true;
			draw();
		},
			trackMouse = function (e) {
				mouse_x = -1;
				mouse_y = -1;

				if (e && e.type === "mousemove") {
					mouse_x = e.clientX;
					mouse_y = e.clientY;
				}

				if (e && e.type === "touchmove") {
					var touch = e.touches[0];
					mouse_x = touch.clientX;
					mouse_y = touch.clientY;
				}

				if (document.body.getBoundingClientRect) {
					var rect = canvas.getBoundingClientRect();
					mouse_x -= rect.left;
					mouse_y -= rect.top;
				}

				for (var i in matrix) {
					var tile = matrix[i];

					if (
						Math.random() < .2 &&
						tile._x <= mouse_x &&
						tile._x + size > mouse_x &&
						tile._y <= mouse_y &&
						tile._y + size > mouse_y
					) {
						_color = getRandomInt(225, 250);
						color = [_color, _color, _color];

						c.fillStyle = "rgba(" + color + ",1)";
						c.beginPath();

						if (tile.x % 2 == 0) {
							c.moveTo(tile._x - size / 2, tile._y);
							c.lineTo(tile._x + size / 2, tile._y);
							c.lineTo(tile._x, tile._y + size);
						} else {
							c.moveTo(tile._x - size / 2, tile._y + size);
							c.lineTo(tile._x, tile._y);
							c.lineTo(tile._x + size / 2, tile._y + size);
						}
						c.closePath();
						c.fill();
					}
				}
			},
			draw = function () {
				init = false;
				var counter = 12;
				if (init) counter = matrix.length;
				now = Date.now();
				delta = now - then;
				if (delta > interval) {
					while (counter > 0) {
						counter = counter - 1;
						var tile = randomElement(matrix);

						_color = getRandomInt(180, 220);
						color = [_color, _color, _color];
						c.fillStyle = "rgba(" + color + ",.1)";

						c.beginPath();

						if (tile.x % 2 == 0) {
							c.moveTo(tile._x - size / 2, tile._y);
							c.lineTo(tile._x + size / 2, tile._y);
							c.lineTo(tile._x, tile._y + size);
						} else {
							c.moveTo(tile._x - size / 2, tile._y + size);
							c.lineTo(tile._x, tile._y);
							c.lineTo(tile._x + size / 2, tile._y + size);
						}
						c.closePath();
						c.fill();
					}

					then = now - (delta % interval);
				}
				window.requestAnimationFrame(draw);
			},
			randomElement = function (array) {
				return array[Math.floor(Math.random() * array.length)];
			},
			getRandomInt = function (min, max) {
				return Math.floor(Math.random() * (max - min + 1)) + min;
			}

		return {
			init: init
		};
	}
};


/*=========================================
=            Calling Functions            =
=========================================*/


/**
 *
 * Document Ready
 *
 **/


; (function () {

	app.common.vars.beforeLoadingFunction = app.home.fn.beforeLoadingFunction;
	app.common.vars.afterLoadingFunction = app.home.fn.afterLoadingFunction;

}());



/**
 *
 * Custom Triggers
 *
 **/


$j(window).on({

	'hashchange': function () {

	},

	'load': function (e) {

	},

	'scroll': function (e) {

	},

	'resize': function (e) {

	},

	'mousemove': function (e) {

	},

	'touchmove': function (e) {

	},

	'mouseup': function (e) {

	},

	'keypress': function (e) {

	},

	'keydown': function (e) {

	},

	'keyup': function (e) {

	},

	'mousewheel DOMMouseScroll': function (e) {

	}

});