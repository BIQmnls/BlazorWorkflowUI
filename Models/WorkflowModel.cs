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
    public Dictionary<string, object> Parameters { get; set; } = new();
    public List<StepAttachmentModel> Attachments { get; set; } = new();
    public string? NextStepId { get; set; }
    public string? ElseNextStepId { get; set; }
}

public class StepAttachmentModel
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = Array.Empty<byte>();
}
