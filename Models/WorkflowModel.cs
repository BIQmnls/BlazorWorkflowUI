namespace BlazorWorkflowUI.Models;

public class WorkflowModel
{
    public string Name { get; set; } = "New Workflow";
    public TriggerModel Trigger { get; set; } = new();
    public List<StepModel> Steps { get; set; } = new();
}

public class TriggerModel
{
    public string ActivityType { get; set; } = "PolicyCreated";
    public string Condition { get; set; } = "NoCondition";
}

public class StepModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ActivityType { get; set; } = "SendPolicyDocument";
    public int? DelaySeconds { get; set; }
    public string Condition { get; set; } = "NoCondition";
    public string? ElseActivityType { get; set; }
    public int? ElseDelaySeconds { get; set; }
}
