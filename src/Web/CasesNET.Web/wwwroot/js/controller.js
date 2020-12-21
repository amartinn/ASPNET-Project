var canvas;
var clipboard;
function init(canvasElement) {
    this.canvas = new fabric.Canvas(canvasElement);
    fabric.Object.prototype.transparentCorners = false;
    fabric.Object.prototype.cornerColor = 'lightblue';
    fabric.Object.prototype.cornerStyle = 'circle';
    fabric.Object.prototype.controls.deleteControl = new fabric.Control({
        x: 0.3,
        y: -0.6,
        offsetY: 16,
        cursorStyle: 'pointer',
        mouseUpHandler: function (eventData, target) {
            const activeItem = target.canvas;

            activeItem.remove(target);
            activeItem.requestRenderAll();
        },
        render: function (ctx, left, top, styleOverride, fabricObject) {
            const icon = document.createElement('img');
            icon.src = "data:image/svg+xml,%3C%3Fxml version='1.0' encoding='utf-8'%3F%3E%3C!DOCTYPE svg PUBLIC '-//W3C//DTD SVG 1.1//EN' 'http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd'%3E%3Csvg version='1.1' id='Ebene_1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' width='595.275px' height='595.275px' viewBox='200 215 230 470' xml:space='preserve'%3E%3Ccircle style='fill:%23F44336;' cx='299.76' cy='439.067' r='218.516'/%3E%3Cg%3E%3Crect x='267.162' y='307.978' transform='matrix(0.7071 -0.7071 0.7071 0.7071 -222.6202 340.6915)' style='fill:white;' width='65.545' height='262.18'/%3E%3Crect x='266.988' y='308.153' transform='matrix(0.7071 0.7071 -0.7071 0.7071 398.3889 -83.3116)' style='fill:white;' width='65.544' height='262.179'/%3E%3C/g%3E%3C/svg%3E";
            const size = this.cornerSize;
            ctx.save();
            ctx.translate(left, top);
            ctx.rotate(fabric.util.degreesToRadians(fabricObject.angle));
            ctx.drawImage(icon, -size / 2, -size / 2, size, size);
            ctx.restore();
        },
        cornerSize: 24
    });
    this.setDefaultOverlayImage();

    const self = this;
    this.canvas.on('after:render', function () {
        const bgImage = self.canvas._objects.find(el => el.name === 'bg-image');
        if (bgImage) {
            self.canvas.sendToBack(bgImage)
        }
    })  
    this.canvas.on('object:modified', function () {
        const bgImage = self.canvas._objects.find(el => el.name === 'bg-image');
        if (bgImage) {
            self.canvas.sendToBack(bgImage)
        }
    })
    this.canvas.on('selection:cleared', function () {
        const bgImage = self.canvas._objects.find(el => el.name === 'bg-image');
        if (bgImage) {
            self.canvas.sendToBack(bgImage)
        }
    })  
}

function setDefaultOverlayImage() {
    this.canvas.setOverlayImage('/overlay.png', this.canvas.renderAll.bind(this.canvas), {
        backgroundImageOpacity: 1,
        scaleX: 1,
        scaleY: 1,
    });
}

function addBackgroundImageFromUrl(data) {
    fabric.Image.fromURL(data, (image) => {
        image.scale(0.2);
        image.set({
            left: 0,
            top: 0,
            hoverCursor: 'default',
            name: 'bg-image'
        })
        this.canvas.add(image).renderAll();
        this.canvas.setActiveObject(image);
    });
}
function lockBackground() {
    const bgImage = this.canvas._objects.find(el => el.name === 'bg-image');
    if (bgImage) {
        bgImage.set('lockMovementY', 3).setCoords();
        bgImage.set('lockMovementX', 3).setCoords();
        bgImage.selectable = false;
    }
}
function addRectangle() {
    const center = this.canvas.getCenter();
    const rectangle = new fabric.Rect({
        width: 100,
        height: 100,
        top: center.top,
        left: center.left,
        fill: getRandomColor(),
        originX: 'center',
        originY: 'center',
    })
    this.canvas.add(rectangle);
    this.canvas.bringToFront(rectangle);
    this.canvas.setActiveObject(rectangle)
    this.canvas.requestRenderAll();
}
function addCircle() {
    const center = this.canvas.getCenter();
    const circle = new fabric.Circle({
        top: center.top,
        left: center.left,
        fill: getRandomColor(),
        radius: 100,
        originX: 'center',
        originY: 'center',
    })
    this.canvas.add(circle);
    this.canvas.bringToFront(circle);
    this.canvas.setActiveObject(circle)
    this.canvas.requestRenderAll();
}
function addTriangle() {
    const center = this.canvas.getCenter();
    const triangle = new fabric.Triangle({
        top: center.top,
        left: center.left,
        fill: getRandomColor(),
        width: 100,
        height: 100,
        originX: 'center',
        originY: 'center',
    })
    this.canvas.add(triangle);
    this.canvas.bringToFront(triangle);
    this.canvas.setActiveObject(triangle)
    this.canvas.requestRenderAll();
}

function addTextbox() {
    var text = 'Lorem ipsum dolor sit amet,\nconsectetur adipisicing elit,\nsed do eiusmod tempor incididunt\nut labore et dolore magna aliqua.\n' +
        'Ut enim ad minim veniam,\nquis nostrud exercitation ullamco\nlaboris nisi ut aliquip ex ea commodo consequat.';
    const center = this.canvas.getCenter();
    var textSample = new fabric.IText(text.slice(0, 30), {
        left: center.left,
        top: center.top,
        fontFamily: 'helvetica',
        fill: '#' + getRandomColor(),
        scaleX: 0.5,
        scaleY: 0.5,
        fontWeight: '',
        originX: 'left',
        hasRotatingPoint: true,
        centerTransform: true
    });

    this.canvas.add(textSample);
    this.canvas.bringToFront(textSample);
    this.canvas.setActiveObject(textSample)
    this.canvas.requestRenderAll();
}
function addEmoji(url) {
    var group = [];
    const self = this;
    const center = this.canvas.getCenter();
    fabric.loadSVGFromURL(url, function (objects, options) {
        var loadedObjects = new fabric.Group(group);
        loadedObjects.set({
            backgroundImageOpacity: 1,
            backgroundImageStretch: false,
            scaleX: 2,
            scaleY: 2,
            top: center.top,
            left: center.left,
            originX: 'center',
            originY: 'center',
            name: 'emoji'
        });
        self.canvas.add(loadedObjects);
        self.canvas.renderAll();
        self.canvas.setActiveObject(loadedObjects);
        self.canvas.bringToFront(loadedObjects);
    },
        function (item, object) {
            object.set('id', item.getAttribute('id'));
            group.push(object);
        });
}

function setOpacity(value) {
        setActiveStyle('opacity', parseInt(value, 10) / 100);
}

function setFontSize (value) {
    setActiveStyle('fontSize', parseInt(value, 10));
};

function setColor(value) {
    setActiveProp('fill',value)
}

function toggleItalic() {
    setActiveStyle('fontStyle',
        getActiveStyle('fontStyle') === 'italic' ? '' : 'italic');
};


function toggleBold() {
    setActiveStyle('fontStyle',
        getActiveStyle('fontStyle') === 'bold' ? '' : 'bold');
};

function toggleUnderline() {    
    setActiveStyle('textDecoration', 5);
    setActiveStyle('underline', !getActiveStyle('underline'));
};

function sendToBack() {
    const activeObject = canvas.getActiveObject();
    this.canvas.sendBackwards(activeObject);
    this.canvas.requestRenderAll();
}
function bringToFront() {
    const activeObject = canvas.getActiveObject();
    this.canvas.bringForward(activeObject);
    this.canvas.requestRenderAll();
}

function Copy() {
    const activeObject = getSelected();
    if (activeObject) {
        const self = this;
        activeObject.clone(function (cloned) {
            self.clipboard = cloned;
        });
    }
}
function Paste() {
    if (this.clipboard) {
        const self = this;
        this.clipboard.clone(function (clonedObj) {
            self.canvas.discardActiveObject();
            clonedObj.set({
                left: clonedObj.left + 10,
                top: clonedObj.top + 10,
                evented: true,
            });
            if (clonedObj.type === 'activeSelection') {
                clonedObj.canvas = self.canvas;
                clonedObj.forEachObject(function (obj) {

                    self.canvas.add(obj);
                });
                clonedObj.setCoords();
            } else {
                self.canvas.add(clonedObj);
            }
            self.clipboard.top += 10;
            self.clipboard.left += 10;
            self.canvas.setActiveObject(clonedObj);
            self.canvas.requestRenderAll();
        });
    }
}

function Delete() {
    var activeObjects = this.canvas.getActiveObjects();
    this.canvas.discardActiveObject();
    if (activeObjects.length) {
        canvas.remove.apply(canvas, activeObjects);
    }
}
function toggleLinethrough() {
    setActiveStyle('textDecoration', 5);
    setActiveStyle('linethrough', !getActiveStyle('linethrough'));
};

function getRandomColor() {
    return "#" + ((1 << 24) * Math.random() | 0).toString(16);
}

function getSelected() {
    return this.canvas.getActiveObject();
}

function setActiveStyle(styleName, value, object) {
    object = object || canvas.getActiveObject();
    if (!object) return;

    if (object.setSelectionStyles && object.isEditing) {
        var style = {};
        style[styleName] = value;
        object.setSelectionStyles(style);
        object.setCoords();
    } else {
        object.set(styleName, value);
    }

    object.setCoords();
    this.canvas.requestRenderAll();
};

function getActiveStyle(styleName, object) {
    object = object || canvas.getActiveObject();
    if (!object) return '';

    return (object.getSelectionStyles && object.isEditing) ?
        (object.getSelectionStyles()[styleName] || '') :
        (object[styleName] || '');
};

function getActiveProp(name) {
    var object = canvas.getActiveObject();
    if (!object) return '';

    return object[name] || '';
}

function setActiveProp(name, value) {
    var object = canvas.getActiveObject();
    if (!object) return;
    object.set(name, value).setCoords();
    this.canvas.renderAll();
}