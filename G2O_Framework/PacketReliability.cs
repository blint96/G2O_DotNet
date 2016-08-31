namespace G2O_Framework
{
    public enum PacketReliability
    {
        /// <summary>
        /// Unreliable packets are sent by straight UDP. They may arrive out of order, or not at all. This is best for data that is unimportant, or data that you send very frequently so even if some packets are missed newer packets will compensate.
        ///   Advantages - These packets don't need to be acknowledged by the network, saving the size of a UDP header in acknowledgment (about 50 bytes or so). The savings can really add up.
        /// Disadvantages - No packet ordering, packets may never arrive, these packets are the first to get dropped if the send buffer is full.
        /// </summary>
        Unreliable, 

        /// <summary>
        /// Reliable packets are UDP packets monitored by a reliability layer to ensure they arrive at the destination.
        /// Advantages - You know the packet will get there. Eventually...
        /// Disadvantages - Retransmissions and acknowledgments can add significant bandwidth requirements. Packets may arrive very late if the network is busy. No packet ordering.
        /// </summary>
        Reliable, 

        /// <summary>
        /// Reliable ordered packets are UDP packets monitored by a reliability layer to ensure they arrive at the destination and are ordered at the destination. Advantages - The packet will get there and in the order it was sent. These are by far the easiest to program for because you don't have to worry about strange behavior due to out of order or lost packets.
        /// Disadvantages - Retransmissions and acknowledgments can add significant bandwidth requirements. Packets may arrive very late if the network is busy. One late packet can delay many packets that arrived sooner, resulting in significant lag spikes. However, this disadvantage can be mitigated by the clever use of ordering streams.
        /// </summary>
        ReliableOrdered
    }
}