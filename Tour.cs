using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Akron.Web.Components.Tour;

[PublicAPI]
public class Tour
{
    private readonly IJSRuntime _runtime;

    public Tour(IJSRuntime runtime)
    {
        _runtime = runtime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="dotnetRef"></param>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    public async Task SetupAsync<T>(string id, DotNetObjectReference<T> dotnetRef, TourOptions options) where T : class
    {
        await _runtime.InvokeVoidAsync("tourSetup", id, dotnetRef, options);
    }

    /// <summary>
    /// Adds a new step to the tour
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="step"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public Task AddStepAsync(string id, TourOptions.Step step, int index)
    {
        return CallMethodAsync(id, "addStep", step, index);
    }

    /// <summary>
    /// Adds a new steps to the tour
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public Task AddStepAsync(string id, List<TourOptions.Step> steps)
    {
        return CallMethodAsync(id, "addSteps", steps);
    }

    /// <summary>
    /// Go to the previous step in the tour
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task BackAsync(string id)
    {
        return CallMethodAsync(id, "back");
    }

    /// <summary>
    /// Calls _done() triggering the 'cancel' event If confirmCancel is true,
    /// will show a window.confirm before cancelling
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task CancelAsync(string id)
    {
        return CallMethodAsync(id, "cancel");
    }

    /// <summary>
    /// Calls _done() triggering the complete event
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task CompleteAsync(string id)
    {
        return CallMethodAsync(id, "complete");
    }

    /// <summary>
    /// Gets the step from a given id
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="stepId">The id of the step to retrieve</param>
    /// <returns></returns>
    public Task<TourOptions.Step> GetByIdAsync(string id, string stepId)
    {
        return CallMethodAsync<TourOptions.Step>(id, "getById", stepId);
    }

    /// <summary>
    /// Gets the current step
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task<TourOptions.Step> GetCurrentStepAsync(string id)
    {
        return CallMethodAsync<TourOptions.Step>(id, "getCurrentStep");
    }

    /// <summary>
    /// Hide the current step
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task HideAsync(string id)
    {
        return CallMethodAsync(id, "hide");
    }

    /// <summary>
    /// Check if the tour is active
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task<bool> IsActiveAsync(string id)
    {
        return CallMethodAsync<bool>(id, "isActive");
    }

    /// <summary>
    /// Go to the next step in the tour If we are at the end, call complete
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task NextAsync(string id)
    {
        return CallMethodAsync(id, "next");
    }

    /// <summary>
    /// Removes the step from the tour
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="name">The id for the step to remove</param>
    /// <returns></returns>
    public Task RemoveStepAsync(string id, string name)
    {
        return CallMethodAsync(id, "removeStep", name);
    }

    /// <summary>
    /// Show a specific step in the tour
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="key">The key to look up the step by</param>
    /// <param name="forward">True if we are going forward, false if backward</param>
    /// <returns></returns>
    public Task ShowAsync(string id, string key = "0", bool forward = true)
    {
        return CallMethodAsync(id, "show", key, forward);
    }

    /// <summary>
    /// Start the tour
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <returns></returns>
    public Task StartAsync(string id)
    {
        return CallMethodAsync(id, "start");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="method"></param>
    /// <param name="args"></param>
    public async Task CallMethodAsync(string id, string method, params object?[]? args)
    {
        await _runtime.InvokeVoidAsync("tourCaller", id, method, args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">The ID of the tour.</param>
    /// <param name="method"></param>
    /// <param name="args"></param>
    public async Task<T> CallMethodAsync<T>(string id, string method, params object?[]? args)
    {
        return await _runtime.InvokeAsync<T>("tourCaller", id, method, args);
    }
}

[PublicAPI]
public class TourOptions
{
    /// <summary>
    /// If true, will issue a window.confirm before cancelling
    /// </summary>
    public bool ConfirmCancel { get; set; }

    /// <summary>
    /// The message to display in the confirm dialog
    /// </summary>
    public string? ConfirmCancelMessage { get; set; }

    /// <summary>
    /// The prefix to add to the shepherd-enabled and shepherd-target class names as well as the data-shepherd-step-id.
    /// </summary>
    public string? ClassPrefix { get; set; }

    /// <summary>
    /// Default options for Steps (Step#constructor), created through addStep
    /// </summary>
    public object? DefaultStepOptions { get; set; }

    /// <summary>
    /// Exiting the tour with the escape key will be enabled unless this is explicitly set to false.
    /// </summary>
    public bool ExitOnEsc { get; set; } = true;

    /// <summary>
    /// Navigating the tour via left and right arrow keys will be enabled unless this is explicitly set to false.
    /// </summary>
    public bool KeyboardNavigation { get; set; } = true;

    /// <summary>
    /// An optional container element for the steps. If not set, the steps will be appended to document.body.
    /// </summary>
    public ElementReference? StepsContainer { get; set; }

    /// <summary>
    /// An optional container element for the modal. If not set, the modal will be appended to document.body.
    /// </summary>
    public ElementReference? ModalContainer { get; set; }

    /// <summary>
    /// An array of step options objects or Step instances to initialize the tour with
    /// </summary>
    public List<Step> Steps { get; set; } = new();

    /// <summary>
    /// An optional "name" for the tour. This will be appended to the the tour's dynamically generated id property.
    /// </summary>
    public string? TourName { get; set; }

    /// <summary>
    /// Whether or not steps should be placed above a darkened modal overlay. If true, the overlay will create an
    /// opening around the target element so that it can remain interactive
    /// </summary>
    public bool UseModalOverlay { get; set; } = true;

    [PublicAPI]
    public class Step
    {
        /// <summary>
        /// Whether to display the arrow for the tooltip or not. Defaults to true.
        /// </summary>
        public bool Arrow { get; set; } = true;

        /// <summary>
        /// The element the step should be attached to on the page.
        /// </summary>
        public AttachTo? AttachTo { get; set; }

        /// <summary>
        /// An action on the page which should advance shepherd to the next step. 
        /// </summary>
        public AdvanceOn? AdvanceOn { get; set; }

        /// <summary>
        /// This might not work. Untested with Blazor
        /// A function that returns a promise. When the promise resolves, the rest of the show code for the step will execute.
        /// </summary>
        public object? BeforeShowPromise { get; set; }

        /// <summary>
        /// An array of buttons to add to the step. These will be rendered in a footer below the main body text.
        /// </summary>
        public List<Button>? Buttons { get; set; }

        /// <summary>
        /// A boolean, that when set to false, will set pointer-events: none on the target
        /// </summary>
        public bool? CanClickTarget { get; set; }

        /// <summary>
        /// Options for the cancel icon
        /// </summary>
        public CancelIcon? CancelIcon { get; set; }

        /// <summary>
        /// A string of extra classes to add to the step's content element.
        /// </summary>
        public string? Classes { get; set; }

        /// <summary>
        /// An extra class to apply to the attachTo element when it is highlighted (that is, when its step is active). You can then target that selector in your CSS.
        /// </summary>
        public string? HighlightClass { get; set; }

        /// <summary>
        /// The string to use as the id for the step.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// An amount of padding to add around the modal overlay opening
        /// </summary>
        public int ModalOverlayOpeningPadding { get; set; }

        /// <summary>
        /// An amount of border radius to add around the modal overlay opening
        /// </summary>
        public int ModalOverlayOpeningRadius { get; set; }

        /// <summary>
        /// Extra options to pass to FloatingUI
        /// </summary>
        public object? FloatingUiOptions { get; set; }

        /// <summary>
        /// Should the element be scrolled to when this step is shown?
        /// If true, uses the default scrollIntoView, if an object, passes that object as the params to scrollIntoView
        /// i.e. {behavior: 'smooth', block: 'center'}
        /// </summary>
        public object ScrollTo { get; set; } = true;

        /// <summary>
        /// A function that lets you override the default scrollTo behavior and define a custom action to do the
        /// scrolling, and possibly other logic.
        /// </summary>
        public object? ScrollToHandler { get; set; }

        /// <summary>
        /// A function that, when it returns true, will show the step. If it returns false, the step will be skipped.
        /// </summary>
        public object? ShowOn { get; set; }

        /// <summary>
        /// The text in the body of the step. It can be one of three types:
        /// - HTML string
        /// - `HTMLElement` object
        /// - `Function` to be executed when the step is built. It must return one the two options above.
        /// </summary>
        public object? Text { get; set; }

        /// <summary>
        /// The step's title. It becomes an h3 at the top of the step. It can be one of two types:
        /// - HTML string
        /// - `Function` to be executed when the step is built. It must return HTML string.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// You can define show, hide, etc events inside when. For example:
        /// <example>
        /// when: {
        ///    show: function() {
        ///       window.scrollTo(0, 0);
        ///    }
        /// }
        /// </example>
        /// </summary>
        public object? When { get; set; }
    }
}

[PublicAPI]
public class CancelIcon
{
    /// <summary>
    /// Should a cancel “✕” be shown in the header of the step?
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// The label to add for aria-label
    /// </summary>
    public string? Label { get; set; }
}

[PublicAPI]
public static class StandardButtons
{
    public static Button Next => new()
    {
        Action = "tour.next",
        Classes = "btn btn-primary"
    };
}

[PublicAPI]
public class Button
{
    /// <summary>
    /// A function executed when the button is clicked on. It is automatically bound to the tour the step is
    /// associated with, so things like this.next will work inside the action.
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Extra classes to apply to the {a}
    /// </summary>
    public string? Classes { get; set; }

    /// <summary>
    /// Should the button be disabled?
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// The aria-label text of the button
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// If true, a shepherd-button-secondary class is applied to the button
    /// </summary>
    public bool Secondary { get; set; }

    /// <summary>
    /// The HTML text of the button
    /// </summary>
    public string? Text { get; set; }
}

[PublicAPI]
public class AdvanceOn
{
    /// <summary>
    /// An element selector string
    /// </summary>
    public string? Selector { get; set; }

    /// <summary>
    /// Event does not have to be an event inside the tour, it can be any event fired on any element on the page.
    /// You can also always manually advance the Tour by calling myTour.next()
    /// </summary>
    public string? Event { get; set; }
}

[PublicAPI]
public class AttachTo //todo strut? record?
{
    /// <summary>
    /// An element selector string
    /// </summary>
    public string? Element { get; set; }

    /// <summary>
    /// The optional direction to place the FloatingUI tooltip relative to the element.
    /// Possible string values: 'top', 'top-start', 'top-end', 'bottom', 'bottom-start', 'bottom-end', 'right', 'right-start', 'right-end', 'left', 'left-start', 'left-end'
    /// </summary>
    public string On { get; set; } = "bottom";
}
