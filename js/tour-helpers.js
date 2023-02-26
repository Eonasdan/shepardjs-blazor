class TourHelpers {
    tours = [];
    
    setup(id, dotnetRef, options) {
        this.tours.push({
            id: id,
            tour: new Shepherd.Tour(options),
            dotnetRef: dotnetRef
        });
    }
    
    callMethod(id, method) {
        const args = [...arguments].slice(2);
        this.tours.find(x => x.id === id).tour[method](...args);
    }
}

const tourHelpers = new TourHelpers();

// noinspection JSUnusedGlobalSymbols
window.tourSetup = (id, dotnetRef, options) => {
    tourHelpers.setup(id, dotnetRef, options)
}

// noinspection JSUnusedGlobalSymbols
window.tourCaller = (id, method, ag) => {
    tourHelpers.callMethod(id, method, ag);
}