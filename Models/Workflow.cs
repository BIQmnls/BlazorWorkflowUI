namespace BlazorWorkflowUI.Models;

public class Workflow
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public Trigger Trigger { get; set; } = new();
    public List<WorkflowStep> Steps { get; set; } = new();
}

public class Trigger
{
    public string Name { get; set; } = string.Empty;
}

public abstract class WorkflowStep
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
}

public class ActionStep : WorkflowStep
{
    public string? NextStepId { get; set; }
}

public class ConditionStep : WorkflowStep
{
    public string? TrueStepId { get; set; }
    public string? FalseStepId { get; set; }
}
