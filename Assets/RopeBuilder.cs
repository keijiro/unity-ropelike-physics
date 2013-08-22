using UnityEngine;
using System.Collections;

public class RopeBuilder : MonoBehaviour
{
    public GameObject prefab;

    GameObject target;

    void Awake ()
    {
        GameObject prevNode = null;

        for (var i = 0; i < 50; i++) {
            var newNode = new GameObject ("node");

            newNode.transform.localPosition = transform.position + new Vector3 (i, 0, 0);

            if (prevNode != null) {
                var child = Instantiate (prefab, newNode.transform.position, newNode.transform.rotation) as GameObject;
                child.transform.parent = newNode.transform;
            }

            var rb = newNode.AddComponent<Rigidbody> ();
            rb.mass = 1.0f;

            if (prevNode == null) {
                rb.isKinematic = true;
            }

            if (prevNode != null) {
                var joint = newNode.AddComponent<ConfigurableJoint> ();
                joint.connectedBody = prevNode.rigidbody;

                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;
            }

            if (i == 25) {
                Camera.main.GetComponent<CameraMove>().SetTarget(newNode);
            }

            prevNode = newNode;
        }

        target = prevNode;
    }

    IEnumerator Start ()
    {
        while (true) {
            target.rigidbody.AddForce (Random.onUnitSphere * 10.0f, ForceMode.Impulse);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
