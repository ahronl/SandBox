(ns most-resent-repository.core)

(def capacity 10)
(def state (atom {}))
(def queue (atom []))

(defn clear []
  (reset! state {})
  (reset! queue []))

(defn update-queue [k] 
    (swap! queue (fn [q] 
                   (->> q 
                        (remove #(= % k)) 
                        (cons k)
                        (take capacity)
                        (into [])))))

(defn get-state [k]
  (update-queue k)
  (get @state k))

(defn put-state [key val]
  (letfn [(remove-latest [] 
            (when (= (count @state) capacity)
              (swap! state 
                     (fn [s] (dissoc s (last @queue))))))
          
          (update-state [k v]
            (swap! state (fn [s] (assoc s k v))))]

          (remove-latest)
          (update-queue key)
          (update-state key val)))
