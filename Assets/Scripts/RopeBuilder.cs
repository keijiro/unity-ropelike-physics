using UnityEngine;
using System.Collections;

public class RopeBuilder : MonoBehaviour
{
    public GameObject prefab;
    public int nodeNum = 50;
    GameObject[] nodes;

    void SetPhysicProperty (GameObject node, GameObject boundTo, bool isFixed)
    {
        var rb = node.AddComponent<Rigidbody> ();
        rb.mass = 1.0f;
        rb.drag = 0.1f;
        rb.angularDrag = 0.1f;

        if (isFixed) {
            rb.isKinematic = true;
        }

        if (boundTo != null) {
            var joint = node.AddComponent<ConfigurableJoint> ();
            joint.connectedBody = boundTo.rigidbody;

            var limit = new SoftJointLimit ();
            limit.limit = 0.1f;
            limit.spring = 40.0f;
            joint.linearLimit = limit;

            limit.limit = 10.0f;
            joint.angularYLimit = limit;
            joint.angularZLimit = limit;
            joint.highAngularXLimit = limit;
            joint.lowAngularXLimit = limit;

            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;
            joint.angularXMotion = ConfigurableJointMotion.Limited;
            joint.angularYMotion = ConfigurableJointMotion.Limited;
            joint.angularZMotion = ConfigurableJointMotion.Limited;
        }
    }

    void Awake ()
    {
        nodes = new GameObject[nodeNum];

        // Make the first node.
        var firstNode = new GameObject ("first node");
        firstNode.transform.localPosition = transform.position;
        SetPhysicProperty (firstNode, null, true);

        // Make the chain of nodes.
        for (var i = 0; i < nodeNum; i++) {
            var node = new GameObject ("node");
            nodes [i] = node;

            node.transform.localPosition = transform.position + new Vector3 (i, 0, 0);

            var child = Instantiate (prefab, node.transform.position, node.transform.rotation) as GameObject;
            child.transform.parent = node.transform;
            child.GetComponent<CubeScaler>().timeOffset = i;

            SetPhysicProperty (node, i == 0 ? firstNode : nodes [i - 1], false);
        }
    }

    IEnumerator Start ()
    {
        while (true) {
            var rb = nodes [Random.Range (0, nodes.Length)].rigidbody;
            rb.AddForceAtPosition (Random.onUnitSphere * 8.0f, rb.position + Random.onUnitSphere * 1.0f, ForceMode.Impulse);
            yield return new WaitForSeconds (Random.Range (0.4f, 2.0f));
        }
    }
}
