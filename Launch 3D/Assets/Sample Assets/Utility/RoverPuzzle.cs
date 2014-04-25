using UnityEngine;
using System.Collections;

public class RoverPuzzle : MonoBehaviour {
	
	//Curiosity
	public GameObject CuriosityRover;
	public GameObject CuriosityGround;
	public bool CuriosityAlive = true;
	public int CuriosityTree = 1;
	//Spirit
	public GameObject SpiritRover;
	public GameObject SpiritGround;
	public bool SpiritAlive = false;
	public int SpiritTree = 5;
	//Opportunity
	public GameObject OpportunityRover;
	public GameObject OpportunityGround;
	public bool OpportunityAlive = false;
	public int OpportunityTree = 5;
	//Sojourner
	public GameObject SojournerRover;
	public GameObject SojournerGround;
	public bool SojournerAlive = false;
	public int SojournerTree = 5;
	//FifthRover
	public GameObject FifthRover;
	public GameObject FifthGround;
	public bool FifthAlive = false;
	public int FifthTree = 5;
	//Selected
	public GameObject SelectedRover;
	public GameObject SelectedGround;
	public bool SelectedAlive = false;
	public int SelectedTree = 7;
	//TempSelected
	public GameObject TempRover;
	public GameObject TempGround;
	public bool TempAlive = false;
	public int TempTree = 7;
	public GameObject InvalidGround;
	public GameObject FinishObj;

	RaycastHit hit;
	public Camera minigameCamera;
	public AudioClip incorrectSound;
	public AudioClip winSound;
	public bool YouWon = false;
	public Material RoverColor;
	public Material RoverSelected;

	public Transform player;
	public GameObject mainCamera;
	public GameObject miniCam;
	private FirstPersonCharacter FPCscript;
	public GameObject RoverPuzzleObject;
	private RoverPuzzle RoverPuzzleScript;
	
	void Start() {
		FPCscript = player.GetComponent<FirstPersonCharacter> ();
		RoverPuzzleScript = RoverPuzzleObject.GetComponent<RoverPuzzle> ();

		hit = new RaycastHit();
		//print ("i am aliiiiiive");
	}
	
	void Update () {
		Alive ();
		Selecting ();
	}
	
	void Selecting() {
	//void OnMouseUp() {
		Ray ray = minigameCamera.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonUp(0)) {
			if (Physics.Raycast (ray, out hit, 100)) { 
				//set the temporary selected rover equal to gameobject in hit
				TempRover = hit.collider.gameObject;
				//print ("hey look i did something is this really working " + TempRover.name);

				//check the temporary object against all rovers
				if (TempRover == CuriosityRover) {
					TempGround = CuriosityGround;
					TempAlive = CuriosityAlive;
					TempTree = CuriosityTree;
				} 
				else if (TempRover == SpiritRover) {
					TempGround = SpiritGround;
					TempAlive = SpiritAlive;
					TempTree = SpiritTree;
				} 
				else if (TempRover == OpportunityRover) {
					TempGround = OpportunityGround;
					TempAlive = OpportunityAlive;
					TempTree = OpportunityTree;
				} 
				else if (TempRover == SojournerRover) {
					TempGround = SojournerGround;
					TempAlive = SojournerAlive;
					TempTree = SojournerTree;
				}
				else if (TempRover == FifthRover) {
					TempGround = FifthGround;
					TempAlive = FifthAlive;
					TempTree = FifthTree;
				}
				
				//if the temp object is actually a rover
				if (TempTree != 7) {
					//if that rover is already selected then deselect it
					if (TempRover == SelectedRover) {
						print ("Deselecting " + SelectedRover.name);
						SelectedRover.renderer.material = RoverColor;
						SelectedRover = null;
						SelectedGround = null;
						SelectedAlive = false;
						SelectedTree = 7;
						TempRover = null;
						TempGround = null;
						TempAlive = false;
						TempTree = 7;
					}
					//if it isn't selected then select it iff it's alive
					else if (TempAlive == true) {
						if (SelectedAlive == true) {
							SelectedRover.renderer.material = RoverColor;
						}
						SelectedRover = TempRover;
						SelectedGround = TempGround;
						SelectedAlive = TempAlive;
						SelectedTree = TempTree;
						SelectedRover.renderer.material = RoverSelected;
						TempRover = null;
						TempGround = null;
						TempAlive = false;
						TempTree = 7;
						print ("Selecting " + SelectedRover.name);
					}
					//if it's a rover but not alive play negative sound
					else {
						print ("This rover is not alive.");
						audio.PlayOneShot (incorrectSound);
					}
				}
				//this means that the object wasn't a rover so check if it's valid ground
				else if (TempRover == CuriosityGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == CuriosityRover) {
							print ("Curiosity was moved.");
							CuriosityRover.transform.position = hit.point;
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				} 
				else if (TempRover == SpiritGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == SpiritRover) {
							if (OpportunityAlive == true) {
								float distance = Vector3.Distance (SpiritRover.transform.position, OpportunityRover.transform.position);
								if (distance <0.5) {
									if (SpiritTree > (OpportunityTree + 1))
										SpiritTree = (OpportunityTree + 1);
									SpiritRover.transform.position = hit.point;
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}
							if (SojournerAlive == true) {
								float distance = Vector3.Distance (SpiritRover.transform.position, SojournerRover.transform.position);
								if (distance <0.5) {
									if (SpiritTree > (SojournerTree + 1))
										SpiritTree = (SojournerTree + 1);
									SpiritRover.transform.position = hit.point;
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}		
							if (FifthAlive == true) {
								float distance = Vector3.Distance (SpiritRover.transform.position, FifthRover.transform.position);
								if (distance <0.5) {
									if (SpiritTree > (FifthTree + 1))
										SpiritTree = (FifthTree + 1);
									SpiritRover.transform.position = hit.point;
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}
							if (CuriosityAlive == true) {
								float distance = Vector3.Distance (SpiritRover.transform.position, CuriosityRover.transform.position);
								if (distance <0.5) {
									if (SpiritTree > (CuriosityTree + 1))
										SpiritTree = (CuriosityTree + 1);
									SpiritRover.transform.position = hit.point;
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}
							else {
								print ("Could not be moved.");
							}
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				} 
				else if (TempRover == OpportunityGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
						if (SelectedTree != 7) {
							if (SelectedRover == OpportunityRover) {
								if (SpiritAlive == true) {
									float distance = Vector3.Distance (OpportunityRover.transform.position, SpiritRover.transform.position);
									if (distance <0.5) {
										if (OpportunityTree > (SpiritTree + 1))
											OpportunityTree = (SpiritTree + 1);
										OpportunityRover.transform.position = hit.point;
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}
								if (SojournerAlive == true) {
									float distance = Vector3.Distance (OpportunityRover.transform.position, SojournerRover.transform.position);
									if (distance <0.5) {
										if (OpportunityTree > (SojournerTree + 1))
											OpportunityTree = (SojournerTree + 1);
										OpportunityRover.transform.position = hit.point;
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}		
								if (FifthAlive == true) {
									float distance = Vector3.Distance (OpportunityRover.transform.position, FifthRover.transform.position);
									if (distance <0.5) {
										if (OpportunityTree > (FifthTree + 1))
											OpportunityTree = (FifthTree + 1);
										OpportunityRover.transform.position = hit.point;
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}	
								if (CuriosityAlive == true) {
									float distance = Vector3.Distance (OpportunityRover.transform.position, CuriosityRover.transform.position);
									if (distance <0.5) {
										if (OpportunityTree > (CuriosityTree + 1))
											OpportunityTree = (CuriosityTree + 1);
										OpportunityRover.transform.position = hit.point;
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}
								else {
									print ("Could not be moved.");
								}
							} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				} 
				else if (TempRover == SojournerGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == SojournerRover) {
							if (SpiritAlive == true) {
								float distance = Vector3.Distance (SojournerRover.transform.position, SpiritRover.transform.position);
								if (distance <0.5) {
									if (SojournerTree > (SpiritTree + 1))
										SojournerTree = (SpiritTree + 1);
									SojournerRover.transform.position = hit.point;
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}
							if (OpportunityAlive == true) {
								float distance = Vector3.Distance (SojournerRover.transform.position, OpportunityRover.transform.position);
								if (distance <0.5) {
									if (SojournerTree > (OpportunityTree + 1))
										SojournerTree = (OpportunityTree + 1);
									SojournerRover.transform.position = hit.point;
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}		
							if (FifthAlive == true) {
								float distance = Vector3.Distance (SojournerRover.transform.position, FifthRover.transform.position);
								if (distance <0.5) {
									if (SojournerTree > (FifthTree + 1))
										SojournerTree = (FifthTree + 1);
									SojournerRover.transform.position = hit.point;
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}
							if (CuriosityAlive == true) {
								float distance = Vector3.Distance (SojournerRover.transform.position, CuriosityRover.transform.position);
								if (distance <0.5) {
									if (SojournerTree > (CuriosityTree + 1))
										SojournerTree = (CuriosityTree + 1);
									SojournerRover.transform.position = hit.point;
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}
							else {
								print ("Could not be moved.");
							}
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				}
				else if (TempRover == FifthGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == FifthRover) {
							if (SpiritAlive == true) {
								float distance = Vector3.Distance (FifthRover.transform.position, SpiritRover.transform.position);
								if (distance <0.5) {
									if (FifthTree > (SpiritTree + 1))
										FifthTree = (SpiritTree + 1);
									FifthRover.transform.position = hit.point;
									print ("Fifth was moved and it's tree is " + FifthTree);
								}
							}
							if (SojournerAlive == true) {
								float distance = Vector3.Distance (FifthRover.transform.position, SojournerRover.transform.position);
								if (distance <0.5) {
									if (FifthTree > (SojournerTree + 1))
										FifthTree = (SojournerTree + 1);
									FifthRover.transform.position = hit.point;
									print ("Fifth was moved and it's tree is " + OpportunityTree);
								}
							}		
							if (OpportunityAlive == true) {
								float distance = Vector3.Distance (FifthRover.transform.position, OpportunityRover.transform.position);
								if (distance <0.5) {
									if (FifthTree > (OpportunityTree + 1))
										FifthTree = (OpportunityTree + 1);
									FifthRover.transform.position = hit.point;
									print ("Fifth was moved and it's tree is " + FifthTree);
								}
							}	
							if (CuriosityAlive == true) {
								float distance = Vector3.Distance (FifthRover.transform.position, CuriosityRover.transform.position);
								if (distance <0.5) {
									if (FifthTree > (CuriosityTree + 1))
										FifthTree = (CuriosityTree + 1);
									FifthRover.transform.position = hit.point;
									print ("Fifth was moved and it's tree is " + FifthTree);
								}
							}
							else {
								print ("Could not be moved.");
							}
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				}
				else if (TempRover == InvalidGround) {
					if (SelectedTree != 7) {
						audio.PlayOneShot (incorrectSound);
						print ("Invalid ground.");
					}
				}
				//if it isn't a rover and isn't valid ground and it isn't invalid ground then it's offscreen
				else {
					PersistantGlobalScript.interactionEnabled = true;
					PersistantGlobalScript.mouseLookEnabled = true;
					PersistantGlobalScript.movementEnabled = true;
					
					FPCscript.lockCursor = true;
					PersistantGlobalScript.minigameActive = false;
					RoverPuzzleObject.collider.enabled=true;
					mainCamera.SetActive(true);
					miniCam.SetActive(false);
					print ("Do the thing.");
					Screen.lockCursor = true;
					RoverPuzzleScript.enabled=false;
				}
			}
		}
	}
																													
	void Alive() {
		//check alive and "tree" position for spirit
		if (OpportunityAlive == true) {
			float distance = Vector3.Distance (SpiritRover.transform.position, OpportunityRover.transform.position);
			if (distance <0.5) {
				SpiritAlive = true;
				if(SpiritTree > (OpportunityTree+1))
					SpiritTree = (OpportunityTree+1);
			}
		}
		if (SojournerAlive == true) {
 			float distance = Vector3.Distance (SpiritRover.transform.position, SojournerRover.transform.position);
			if (distance <0.5) {
				SpiritAlive = true;
				if(SpiritTree > (SojournerTree+1))
					SpiritTree = (SojournerTree+1);
			}
		}
		if (CuriosityAlive == true) {
			float distance = Vector3.Distance (SpiritRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.5) {
				SpiritAlive = true;
				if(SpiritTree > (CuriosityTree+1))
					SpiritTree = (CuriosityTree+1);
			}
		}
		if (FifthAlive == true) {
			float distance = Vector3.Distance (SpiritRover.transform.position, FifthRover.transform.position);
			if (distance <0.5) {
				SpiritAlive = true;
				if(SpiritTree > (FifthTree+1))
					SpiritTree = (FifthTree+1);
			}
		}
		//check alive and "tree" position for opportunity 
		if (SpiritAlive == true) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, SpiritRover.transform.position);
			if (distance <0.5) {
				OpportunityAlive = true;
				if(OpportunityTree > (SpiritTree+1))
					OpportunityTree = (SpiritTree+1);
			}
		}
		if (SojournerAlive == true) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, SojournerRover.transform.position);
			if (distance <0.5) {
				OpportunityAlive = true;
				if(OpportunityTree > (SojournerTree+1))
					OpportunityTree = (SojournerTree+1);
			}
		}
		if (CuriosityAlive == true) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.5) {
				OpportunityAlive = true;
				if(OpportunityTree > (CuriosityTree+1))
					OpportunityTree = (CuriosityTree+1);
			}
		}
		if (FifthAlive == true) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, FifthRover.transform.position);
			if (distance <0.5) {
				OpportunityAlive = true;
				if(OpportunityTree > (FifthTree+1))
					OpportunityTree = (FifthTree+1);
			}
		}
		//check alive and "tree" position and win condition for sojourner
		if (SpiritAlive == true) {
			float distance = Vector3.Distance (SojournerRover.transform.position, SpiritRover.transform.position);
			if (distance <0.5) {
				SojournerAlive = true;
				if(SojournerTree > (SpiritTree + 1))
					SojournerTree = (SpiritTree + 1);
			}
		}
		if (OpportunityAlive == true) {
			float distance = Vector3.Distance (SojournerRover.transform.position, OpportunityRover.transform.position);
			if (distance <0.5) {
				SojournerAlive = true;
				if(SojournerTree > (OpportunityTree + 1))
					SojournerTree = (OpportunityTree + 1);
			}
		}
		if (CuriosityAlive == true) {
			float distance = Vector3.Distance (SojournerRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.5) {
				SojournerAlive = true;
				if(SojournerTree > (CuriosityTree + 1))
					SojournerTree = (CuriosityTree + 1);
			}
		}
		if (FifthAlive == true) {
			float distance = Vector3.Distance (SojournerRover.transform.position, FifthRover.transform.position);
			if (distance <0.5) {
				SojournerAlive = true;
				if(SojournerTree > (FifthTree+1))
					SojournerTree = (FifthTree+1);
			}
		}
		if (YouWon == false ) {
			float distance = Vector3.Distance (SojournerRover.transform.position, FinishObj.transform.position);
		//	print ("Checking false, distance is: " + distance);
			if (distance > 0.7) {
				audio.PlayOneShot (winSound);
				print ("You win!");
				YouWon = true;

				PersistantGlobalScript.interactionEnabled = true;
				PersistantGlobalScript.mouseLookEnabled = true;
				PersistantGlobalScript.movementEnabled = true;

				FPCscript.lockCursor = true;
				PersistantGlobalScript.minigameActive = false;
				RoverPuzzleObject.collider.enabled=true;
				mainCamera.SetActive(true);
				miniCam.SetActive(false);
				print ("Do the thing.");
				Screen.lockCursor = true;
				RoverPuzzleScript.enabled=false;

				//probably trigger some dialogue thing
			}
		}
		//check alive and "tree" position for fifth
		if (SpiritAlive == true) {
			float distance = Vector3.Distance (FifthRover.transform.position, SpiritRover.transform.position);
			if (distance <0.5) {
				FifthAlive = true;
				if(FifthTree > (SpiritTree + 1))
					FifthTree = (SpiritTree + 1);
			}
		}
		if (OpportunityAlive == true) {
			float distance = Vector3.Distance (FifthRover.transform.position, OpportunityRover.transform.position);
			if (distance <0.5) {
				FifthAlive = true;
				if(FifthTree > (OpportunityTree + 1))
					FifthTree = (OpportunityTree + 1);
			}
		}
		if (CuriosityAlive == true) {
			float distance = Vector3.Distance (FifthRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.5) {
				FifthAlive = true;
				if(FifthTree > (CuriosityTree + 1))
					FifthTree = (CuriosityTree + 1);
			}
		}
		if (SojournerAlive == true) {
			float distance = Vector3.Distance (FifthRover.transform.position, SojournerRover.transform.position);
			if (distance <0.5) {
				FifthAlive = true;
				if(FifthTree > (SojournerTree+1))
					FifthTree = (SojournerTree+1);
			}
		}
		//no need to check for curiosity as it is alwasy root
	}
}