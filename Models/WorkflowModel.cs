namespace BlazorWorkflowUI.Models;

public class WorkflowModel
{
    public string Name { get; set; } = "New Workflow";
    public TriggerModel Trigger { get; set; } = new();
    public List<StepModel> Steps { get; set; } = new();
}

public class TriggerModel
{
    public string ActivityType { get; set; } = "Start";
    public string Condition { get; set; } = "Always";
}

public class StepModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ActivityType { get; set; } = "WriteLine";
    public int? DelaySeconds { get; set; }
}
