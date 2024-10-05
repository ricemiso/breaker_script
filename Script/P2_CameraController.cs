public class P2_CameraController : CameraControllerBase
{
    protected override string CameraCubeName => "Player2_CameraCube";
    protected override string FighterName => "Player2_Fighter";
    protected override System.Type ControllerType => typeof(P2_Controller_Platform);
}