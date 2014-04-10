function findOldestRecipe() {
    var cursor = db.Recipes.find().sort({ dateAdded: 1 });

    printjson(cursor.next());
}

function randomizeSelection(theArray) {
    var selection = Math.floor(Math.random() * theArray.length);
    return theArray[selection];
}

function randomDate(start, end) {
    return new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
}

function randomServingSize() {
    return Math.floor(Math.random() * 12);
}

function randomizeSteps() {
    var numSteps = Math.floor(Math.random() * 100);
    var cookingAction = ["chop", "fry", "pan fry", "mince", "deep fry", "braise", "boil", "slice", "ferment", "bake", "broil", "roast", "slow cook", "sous vide", "juilenne", "simmer", "poach", "grill", "steam", "pickle"];
    var stepsString = "";
    var steps = new Array();
    
    for (var x = 1; x <= numSteps; x++) {
        steps.push(cookingAction[(Math.floor(Math.random() * cookingAction.length))]);
    }
   
    return steps;
}

function buildRecipes(num) {
    var mainIngredients = ["beef", "chicken", "fish", "vegetable", "legume", "fruit", "nut", "pork", "grain", "duck", "buffalo", "deer", "rabbit"];
    var recipeTypes = ["entree", "appetizer", "dessert", "snack", "salad"];
    var recipeRegions = ["American", "French", "Mexican", "Italian", "Chinese", "French", "German", "Spanish", "Thai", "Japanese", "British", "African", "Korean", "Russian", "Portugese"];

    for (var x = 0; x < num; x++) {
        var mainIngredient = randomizeSelection(mainIngredients);
        var recipeType = randomizeSelection(recipeTypes);
        var recipeRegion = randomizeSelection(recipeRegions);

        var recipe = {
            name: "Recipe #" + x,
            dateAdded: randomDate(new Date(2009, 0, 1), new Date()),
            servingSize: randomServingSize(),
            tags: [mainIngredient, recipeType, recipeRegion],
            steps: randomizeSteps()
        };

        db.Recipes.insert(recipe);
    }
}