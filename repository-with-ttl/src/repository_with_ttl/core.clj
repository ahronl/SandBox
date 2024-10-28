(ns repository-with-ttl.core
  (:require [clj-time.core :as t]
            [clojure.core.async :as a]))

(def ^:private my-state (atom {}))

(defn- periodic-clean [n]
   (let [ks (->> 
              @my-state
              (filter (fn [[k v]] 
                        (t/before? (:time v) (t/minus (t/now) (t/minutes n)))))
              (keys)
              (into #{}))]
     (swap! my-state (fn [s] (->> 
                               s
                               (remove (fn [[k _]] (contains? ks k)))
                               (into {}))))))

(defn- run-periodicly [f]
  (a/go-loop []
             (f)
             (Thread/sleep 1000)
             (recur)))

(defn init [n]
  (run-periodicly (partial periodic-clean n)))

(defn put-in-state [k v]
  (swap! my-state assoc k {:val v :time (t/now)}))

(defn get-from-state [k]
  (-> @my-state
      (get k)
      (get :val)))
