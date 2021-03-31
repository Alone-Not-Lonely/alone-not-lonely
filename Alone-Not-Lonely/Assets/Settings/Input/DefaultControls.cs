// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/DefaultControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultControls"",
    ""maps"": [
        {
            ""name"": ""Platforming"",
            ""id"": ""14b64a67-cfbf-4113-be54-77da575b4ef1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""133322ce-494d-4b30-a60a-830a4ee4fbb8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9e4f973b-2f13-496e-bc1b-c6ce0ef67205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""Value"",
                    ""id"": ""e11d468c-2bbc-400f-8431-0f52cd2718c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""92e30b5d-927f-49f7-818f-893edb07e096"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""23a1d8a2-6c60-4a7b-a4fc-11e8e606d15a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveHoriz"",
                    ""type"": ""Value"",
                    ""id"": ""3e3dba55-3d30-403d-9656-744c4d77b55a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveVert"",
                    ""type"": ""Value"",
                    ""id"": ""9e48e90d-28d4-4947-8bb9-273e511a61d5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractionTest"",
                    ""type"": ""Button"",
                    ""id"": ""fde69769-008a-4e34-86e1-761ebb8cd747"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73248d33-6f36-4bc4-8f6c-4a19596aaae6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""772480e4-0988-422a-ba36-ea0a94d645ac"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d2cefc51-bd6c-4c3d-bbbe-1d042bde9475"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02f10d48-44f5-4f3a-89ee-5958bde2e3bd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""36250240-3dad-4654-942f-05c177e91350"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0a931df-d436-42be-aea1-21fda7ae2566"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a1d701a1-486a-4439-a561-1d8943a086c2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da475291-fcb7-4ee5-9040-a0bf123afffb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4817f14f-e4f5-4cbc-a635-269729816772"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80646528-4bc6-4eaf-a6f0-737620e1b440"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9038aebf-e45d-472b-9e88-dd9ee51228a3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c6f4a6d-e792-41c0-a9e1-d94e2369310f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99362815-f727-4074-9408-e7f0010b6339"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""283219c0-95ac-4d3c-a8fe-9225d20f0092"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftStick [Gamepad]"",
                    ""id"": ""c7ea81e8-a227-42bc-b6e2-9e5244ec20ba"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHoriz"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a2568ff4-5f0c-430c-a15b-95d45ef6feb8"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveHoriz"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a9547259-0c77-4444-bb38-b3db0aed9c97"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveHoriz"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a0514df0-f6a4-40b9-b7cb-d8f0d25d5f5e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHoriz"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""48ef4db5-ec2c-4988-9961-ac41eb82f9ea"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveHoriz"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f25da8e2-94f0-4827-aadd-da559f125be7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveHoriz"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick [Gamepad]"",
                    ""id"": ""f628c82a-b750-4d44-afac-6b0132661810"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVert"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a9352f61-2cb0-4e05-bfcc-b414b9c51287"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveVert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4f0f86ab-8aca-4be5-b5ae-ac141d9bb053"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveVert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d5f7b2da-ca93-4834-8359-bb7ddbfcb2bc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVert"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ca9f8662-e92b-41c3-9f17-a3273acb1c0b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveVert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b6643b22-5404-481e-a0a8-ab349b2c1db1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""MoveVert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1415f803-9e0a-4b29-8cea-a26ab7ac6eef"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eef8e2e7-3907-48cb-96d2-cccf7c567031"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ViewingObject"",
            ""id"": ""61f28e81-ae3f-4b8a-9067-f7c114fb1520"",
            ""actions"": [
                {
                    ""name"": ""InteractionTest"",
                    ""type"": ""Button"",
                    ""id"": ""673cd040-18c7-4a21-834d-0dc20b104702"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateObj"",
                    ""type"": ""Value"",
                    ""id"": ""76756476-36cb-41d5-975f-9edd1d989cda"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""74abc05b-4fbc-4c88-8b20-af16cabc0564"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""834850f9-7c1b-4ff2-848f-e6f09282af46"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""375eca1d-09e6-4ca3-b7a6-e1e8311c8f53"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""523be86c-b9fb-4ba3-94c2-800cad783b81"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""RotateObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0be083ed-0b5d-43d3-a310-7bc3188b5298"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""RotateObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""DefaultControlScheme"",
            ""bindingGroup"": ""DefaultControlScheme"",
            ""devices"": []
        }
    ]
}");
        // Platforming
        m_Platforming = asset.FindActionMap("Platforming", throwIfNotFound: true);
        m_Platforming_Move = m_Platforming.FindAction("Move", throwIfNotFound: true);
        m_Platforming_Jump = m_Platforming.FindAction("Jump", throwIfNotFound: true);
        m_Platforming_Camera = m_Platforming.FindAction("Camera", throwIfNotFound: true);
        m_Platforming_Pause = m_Platforming.FindAction("Pause", throwIfNotFound: true);
        m_Platforming_Use = m_Platforming.FindAction("Use", throwIfNotFound: true);
        m_Platforming_MoveHoriz = m_Platforming.FindAction("MoveHoriz", throwIfNotFound: true);
        m_Platforming_MoveVert = m_Platforming.FindAction("MoveVert", throwIfNotFound: true);
        m_Platforming_InteractionTest = m_Platforming.FindAction("InteractionTest", throwIfNotFound: true);
        // ViewingObject
        m_ViewingObject = asset.FindActionMap("ViewingObject", throwIfNotFound: true);
        m_ViewingObject_InteractionTest = m_ViewingObject.FindAction("InteractionTest", throwIfNotFound: true);
        m_ViewingObject_RotateObj = m_ViewingObject.FindAction("RotateObj", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Platforming
    private readonly InputActionMap m_Platforming;
    private IPlatformingActions m_PlatformingActionsCallbackInterface;
    private readonly InputAction m_Platforming_Move;
    private readonly InputAction m_Platforming_Jump;
    private readonly InputAction m_Platforming_Camera;
    private readonly InputAction m_Platforming_Pause;
    private readonly InputAction m_Platforming_Use;
    private readonly InputAction m_Platforming_MoveHoriz;
    private readonly InputAction m_Platforming_MoveVert;
    private readonly InputAction m_Platforming_InteractionTest;
    public struct PlatformingActions
    {
        private @DefaultControls m_Wrapper;
        public PlatformingActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Platforming_Move;
        public InputAction @Jump => m_Wrapper.m_Platforming_Jump;
        public InputAction @Camera => m_Wrapper.m_Platforming_Camera;
        public InputAction @Pause => m_Wrapper.m_Platforming_Pause;
        public InputAction @Use => m_Wrapper.m_Platforming_Use;
        public InputAction @MoveHoriz => m_Wrapper.m_Platforming_MoveHoriz;
        public InputAction @MoveVert => m_Wrapper.m_Platforming_MoveVert;
        public InputAction @InteractionTest => m_Wrapper.m_Platforming_InteractionTest;
        public InputActionMap Get() { return m_Wrapper.m_Platforming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlatformingActions set) { return set.Get(); }
        public void SetCallbacks(IPlatformingActions instance)
        {
            if (m_Wrapper.m_PlatformingActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Camera.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCamera;
                @Pause.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPause;
                @Use.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnUse;
                @MoveHoriz.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMoveHoriz;
                @MoveHoriz.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMoveHoriz;
                @MoveHoriz.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMoveHoriz;
                @MoveVert.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMoveVert;
                @MoveVert.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMoveVert;
                @MoveVert.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMoveVert;
                @InteractionTest.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnInteractionTest;
                @InteractionTest.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnInteractionTest;
                @InteractionTest.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnInteractionTest;
            }
            m_Wrapper.m_PlatformingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @MoveHoriz.started += instance.OnMoveHoriz;
                @MoveHoriz.performed += instance.OnMoveHoriz;
                @MoveHoriz.canceled += instance.OnMoveHoriz;
                @MoveVert.started += instance.OnMoveVert;
                @MoveVert.performed += instance.OnMoveVert;
                @MoveVert.canceled += instance.OnMoveVert;
                @InteractionTest.started += instance.OnInteractionTest;
                @InteractionTest.performed += instance.OnInteractionTest;
                @InteractionTest.canceled += instance.OnInteractionTest;
            }
        }
    }
    public PlatformingActions @Platforming => new PlatformingActions(this);

    // ViewingObject
    private readonly InputActionMap m_ViewingObject;
    private IViewingObjectActions m_ViewingObjectActionsCallbackInterface;
    private readonly InputAction m_ViewingObject_InteractionTest;
    private readonly InputAction m_ViewingObject_RotateObj;
    public struct ViewingObjectActions
    {
        private @DefaultControls m_Wrapper;
        public ViewingObjectActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @InteractionTest => m_Wrapper.m_ViewingObject_InteractionTest;
        public InputAction @RotateObj => m_Wrapper.m_ViewingObject_RotateObj;
        public InputActionMap Get() { return m_Wrapper.m_ViewingObject; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ViewingObjectActions set) { return set.Get(); }
        public void SetCallbacks(IViewingObjectActions instance)
        {
            if (m_Wrapper.m_ViewingObjectActionsCallbackInterface != null)
            {
                @InteractionTest.started -= m_Wrapper.m_ViewingObjectActionsCallbackInterface.OnInteractionTest;
                @InteractionTest.performed -= m_Wrapper.m_ViewingObjectActionsCallbackInterface.OnInteractionTest;
                @InteractionTest.canceled -= m_Wrapper.m_ViewingObjectActionsCallbackInterface.OnInteractionTest;
                @RotateObj.started -= m_Wrapper.m_ViewingObjectActionsCallbackInterface.OnRotateObj;
                @RotateObj.performed -= m_Wrapper.m_ViewingObjectActionsCallbackInterface.OnRotateObj;
                @RotateObj.canceled -= m_Wrapper.m_ViewingObjectActionsCallbackInterface.OnRotateObj;
            }
            m_Wrapper.m_ViewingObjectActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InteractionTest.started += instance.OnInteractionTest;
                @InteractionTest.performed += instance.OnInteractionTest;
                @InteractionTest.canceled += instance.OnInteractionTest;
                @RotateObj.started += instance.OnRotateObj;
                @RotateObj.performed += instance.OnRotateObj;
                @RotateObj.canceled += instance.OnRotateObj;
            }
        }
    }
    public ViewingObjectActions @ViewingObject => new ViewingObjectActions(this);
    private int m_DefaultControlSchemeSchemeIndex = -1;
    public InputControlScheme DefaultControlSchemeScheme
    {
        get
        {
            if (m_DefaultControlSchemeSchemeIndex == -1) m_DefaultControlSchemeSchemeIndex = asset.FindControlSchemeIndex("DefaultControlScheme");
            return asset.controlSchemes[m_DefaultControlSchemeSchemeIndex];
        }
    }
    public interface IPlatformingActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
        void OnMoveHoriz(InputAction.CallbackContext context);
        void OnMoveVert(InputAction.CallbackContext context);
        void OnInteractionTest(InputAction.CallbackContext context);
    }
    public interface IViewingObjectActions
    {
        void OnInteractionTest(InputAction.CallbackContext context);
        void OnRotateObj(InputAction.CallbackContext context);
    }
}
